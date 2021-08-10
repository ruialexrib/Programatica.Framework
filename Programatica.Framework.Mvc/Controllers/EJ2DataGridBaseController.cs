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

        public EJ2DataGridBaseController()
        {
        }

        abstract protected IQueryable<T> LoadData();

        public virtual ActionResult UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IQueryable DataSource = LoadData();

            DataOperations operation = new DataOperations();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                DataSource = (IQueryable)operation.PerformSearching(DataSource, dm.Search);  //Search
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                DataSource = (IQueryable)operation.PerformSorting(DataSource, dm.Sorted);
            }

            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                DataSource = (IQueryable)operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
            }

            int count = DataSource.Cast<T>().Count();

            if (dm.Skip != 0)
            {
                DataSource = (IQueryable)operation.PerformSkip(DataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                DataSource = (IQueryable)operation.PerformTake(DataSource, dm.Take);
            }

            return dm.RequiresCounts
                    ? Json(new { result = DataSource, count })
                    : Json(DataSource);
        }
    }
}
