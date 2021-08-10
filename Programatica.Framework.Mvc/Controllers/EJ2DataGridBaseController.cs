using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Programatica.Framework.Data.Models;
using Syncfusion.EJ2.Base;
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

        public virtual async Task<ActionResult> UrlDatasource([FromBody] DataManagerRequest dm)
        {
            IQueryable<T> dataSource = LoadData();

            DataOperations operation = new DataOperations();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                dataSource = (IQueryable<T>)operation.PerformSearching(dataSource, dm.Search);  //Search
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
            {
                dataSource = (IQueryable<T>)operation.PerformSorting(dataSource, dm.Sorted);
            }

            if (dm.Where != null && dm.Where.Count > 0) //Filtering
            {
                dataSource = (IQueryable<T>)operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }

            int count = dataSource.Cast<T>().Count();

            if (dm.Skip != 0)
            {
                dataSource = (IQueryable<T>)operation.PerformSkip(dataSource, dm.Skip);   //Paging
            }
            if (dm.Take != 0)
            {
                dataSource = (IQueryable<T>)operation.PerformTake(dataSource, dm.Take);
            }

            var result = await dataSource.ToListAsync(); // execute async

            return dm.RequiresCounts
                    ? Json(new { result = result, count })
                    : Json(result);
        }
    }
}
