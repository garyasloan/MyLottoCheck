using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using MyLottoCheck.Models;

namespace MyLottoCheck.Api
{
    public class CaliforniaMegaMillionsWinningNumbersController : ODataController
    {
        private MyLottoCheckModels db = new MyLottoCheckModels();

        // GET: odata/CaliforniaMegaMillionsWinningNumbers
        [EnableQuery]
        public IQueryable<CaliforniaMegaMillionsAllWinningNumber> GetCaliforniaMegaMillionsWinningNumbers()
        {
            return db.CaliforniaMegaMillionsAllWinningNumbers;
        }

        // GET: odata/CaliforniaMegaMillionsWinningNumbers(5)
        [EnableQuery]
        public SingleResult<CaliforniaMegaMillionsAllWinningNumber> GetCaliforniaMegaMillionsAllWinningNumber([FromODataUri] Guid key)
        {
            return SingleResult.Create(db.CaliforniaMegaMillionsAllWinningNumbers.Where(californiaMegaMillionsAllWinningNumber => californiaMegaMillionsAllWinningNumber.Id == key));
        }

        // PUT: odata/CaliforniaMegaMillionsWinningNumbers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] Guid key, Delta<CaliforniaMegaMillionsAllWinningNumber> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CaliforniaMegaMillionsAllWinningNumber californiaMegaMillionsAllWinningNumber = await db.CaliforniaMegaMillionsAllWinningNumbers.FindAsync(key);
            if (californiaMegaMillionsAllWinningNumber == null)
            {
                return NotFound();
            }

            patch.Put(californiaMegaMillionsAllWinningNumber);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaliforniaMegaMillionsAllWinningNumberExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(californiaMegaMillionsAllWinningNumber);
        }

        // POST: odata/CaliforniaMegaMillionsWinningNumbers
        public async Task<IHttpActionResult> Post(CaliforniaMegaMillionsAllWinningNumber californiaMegaMillionsAllWinningNumber)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CaliforniaMegaMillionsAllWinningNumbers.Add(californiaMegaMillionsAllWinningNumber);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CaliforniaMegaMillionsAllWinningNumberExists(californiaMegaMillionsAllWinningNumber.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(californiaMegaMillionsAllWinningNumber);
        }

        // PATCH: odata/CaliforniaMegaMillionsWinningNumbers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] Guid key, Delta<CaliforniaMegaMillionsAllWinningNumber> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CaliforniaMegaMillionsAllWinningNumber californiaMegaMillionsAllWinningNumber = await db.CaliforniaMegaMillionsAllWinningNumbers.FindAsync(key);
            if (californiaMegaMillionsAllWinningNumber == null)
            {
                return NotFound();
            }

            patch.Patch(californiaMegaMillionsAllWinningNumber);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CaliforniaMegaMillionsAllWinningNumberExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(californiaMegaMillionsAllWinningNumber);
        }

        // DELETE: odata/CaliforniaMegaMillionsWinningNumbers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] Guid key)
        {
            CaliforniaMegaMillionsAllWinningNumber californiaMegaMillionsAllWinningNumber = await db.CaliforniaMegaMillionsAllWinningNumbers.FindAsync(key);
            if (californiaMegaMillionsAllWinningNumber == null)
            {
                return NotFound();
            }

            db.CaliforniaMegaMillionsAllWinningNumbers.Remove(californiaMegaMillionsAllWinningNumber);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CaliforniaMegaMillionsAllWinningNumberExists(Guid key)
        {
            return db.CaliforniaMegaMillionsAllWinningNumbers.Count(e => e.Id == key) > 0;
        }
    }
}
