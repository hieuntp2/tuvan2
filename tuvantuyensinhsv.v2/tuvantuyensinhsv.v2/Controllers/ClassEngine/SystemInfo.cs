using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using tuvantuyensinhsv.v2.Models;
namespace tuvantuyensinhsv.v2.Controllers
{
    public class SystemInfo
    { 
        ProjectHEntities db = new ProjectHEntities();
        public async Task AddValue(string key, int value)
        {
            var watch = Stopwatch.StartNew();
            SystemInformation info = db.SystemInformations.SingleOrDefault(t => t.Key == key);

            if (info == null)
            {
                info = new SystemInformation();
                info.Key = key;
                info.value = value;
                db.SystemInformations.Add(info);
                await db.SaveChangesAsync();               
            }
            else
            {
                info.value += value;
                db.Entry(info).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
            }
            var elapsedMs = watch.ElapsedMilliseconds;
        }
        public void SetValue(string key, int value)
        {
            SystemInformation info = db.SystemInformations.SingleOrDefault(t => t.Key == key);

            if (info == null)
            {
                info = new SystemInformation();
                info.Key = key;
                info.value = value;
                db.SystemInformations.Add(info);
                db.SaveChanges();
            }
            else
            {
                info.value = value;
                db.Entry(info).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public void RemoveValue(string key)
        {
            SystemInformation info = db.SystemInformations.SingleOrDefault(t => t.Key == key);

            if (info == null)
            {
                return;
            }
            else
            {
                db.SystemInformations.Remove(info);
                db.SaveChanges();
            }
        }  
    }
}