using Microsoft.AspNetCore.Mvc;
using Programatica.Framework.Data.Models;
using Programatica.Framework.Services;
using Syncfusion.EJ2.Base;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace Programatica.Framework.Mvc.Controllers
{
    public class EJ2DataGridBaseController<T> : BaseController
        where T : IModel
    {
        protected readonly IServiceAsync<T> _modelService;

        public EJ2DataGridBaseController(IServiceAsync<T> modelService)
        {
            _modelService = modelService;
        }

        public virtual async Task<ActionResult> UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = await _modelService.GetAsync();

            DataOperations operation = new DataOperations();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = operation.PerformSorting(DataSource, dm.Sorted);
            }

            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }

            int count = DataSource.Cast<T>().Count();

            if (dm.Skip != 0)
            {
                DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = operation.PerformTake(DataSource, dm.Take);
            }

            return dm.RequiresCounts
                    ? Json(new { result = DataSource, count })
                    : Json(DataSource);
        }
    }
}
