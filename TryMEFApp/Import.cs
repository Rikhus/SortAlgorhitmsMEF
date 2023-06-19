using Protocol;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TryMEFApp
{
    /// <summary>
    /// Позволяет проверять статус импорта
    /// </summary>
    public class Import
    {
        /// <summary>
        /// Физическая папка, где хранятся провайдеры (по умолчанию та, где хранится библиотека с этим классом)
        /// </summary>
        internal static DirectoryCatalog DirectoryCatalog;
        /// <summary>
        /// Виртуальный контейнер, в котором хранятся провайдеры, делается из папки
        /// </summary>
        internal static CompositionContainer HostContainer;
        /// <summary>
        /// Менеджер для работы с экземплярами библиотек провайдеров
        /// </summary>
        static ImportManager<ISort> SortManager;
        /// <summary>
        /// Загрузка провайдеров. Инициализируются хост и менеджер, которые загружают
        /// конкретные значения из библиотек
        /// </summary>
        /// <returns>Список провайдеров</returns>
        public static List<ISort> LoadAndGetSortAlgorhytms()
        {
            DirectoryCatalog = new DirectoryCatalog(Environment.CurrentDirectory);
            HostContainer = new CompositionContainer(DirectoryCatalog);
            SortManager = new ImportManager<ISort>();
            //При успешном импорте хост соберет модели
            SortManager.ImportSatisfied += (sender, e) => { };
            try
            {
                HostContainer.ComposeParts(SortManager);
            }
            catch (CompositionException ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (ReflectionTypeLoadException ex)
            {
                Exception[] Exceptions = ex.LoaderExceptions;
                foreach (Exception curEx in Exceptions)
                {
                    string curMessage = curEx.Message;
                    Type CurType = curEx.GetType();
                    Console.WriteLine(curMessage);
                }
                return null;
            }
            DirectoryCatalog.Refresh();
            //Инициализация программных объектов провайдеров из загруженных
            List<ISort> models = new List<ISort>();
            try
            {
                foreach (Lazy<ISort> extension in
               SortManager.readerExtCollection)
                {
                    models.Add(extension.Value);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
            return models;
        }
    }
    /// <summary>
    /// Позволяет проверять статус импорта
    /// </summary>
    public class ImportEventArgs : EventArgs
    {
        private string status;
        public string Status { get { return status; } set { status = value; } }
        public ImportEventArgs(string status)
        {
            this.status = status;
        }
    }
    /// <summary>
    /// Отвечает за сбор расширений и содержит в себе их коллекцию.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImportManager<T> : IPartImportsSatisfiedNotification where T :
   IExport
    {
        /// <summary>
        /// Статус импорта
        /// </summary>
        public event EventHandler<ImportEventArgs> ImportSatisfied;
        /// <summary>
        /// Коллекция расширений, которая имеет отложенную инициализацию и позволяет пересобирать
        /// себя при обновлении директории расширений
        /// </summary>
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Lazy<T>> readerExtCollection { get; set; }
        public void OnImportsSatisfied()
        {
            ImportSatisfied?.Invoke(this, new ImportEventArgs("Модуль загружен успешно"));
        }
    }


}
