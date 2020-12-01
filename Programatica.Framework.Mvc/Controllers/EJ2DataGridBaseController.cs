using Microsoft.AspNetCore.Mvc;
using Programatica.Framework.Data.Models;
using Syncfusion.EJ2.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programatica.Framework.Mvc.Controllers
{
    public abstract class EJ2DataGridBaseController<T> : BaseController
        where T : IModel
    {
        //protected readonly IService<T> _modelService;

        public EJ2DataGridBaseController()
        {
            //_modelService = modelService;
        }

        abstract protected Task<IEnumerable<T>> LoadDataAsync();

        public virtual async Task<ActionResult> UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IEnumerable DataSource = await LoadDataAsync();

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
