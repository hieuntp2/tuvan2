using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using tuvantuyensinhsv.v2.Models;
using tuvantuyensinhsv.v2.Models.Metadata;
namespace tuvantuyensinhsv.v2.Controllers.ClassEngine
{
    //public static class SearchV2
    //{

    //    static readonly Func<ProjectHEntities, string[], IQueryable<SearchKeyResult>> s_compiledQuery2 =
    //           CompiledQuery.Compile<ProjectHEntities, string[], IQueryable<SearchKeyResult>>(
    //                  (db, words) => from item in db.TruongNganhs.AsNoTracking()
    //                                 where words.All(val => item.Nganh.Ten.Contains(val))
    //                                 select new SearchKeyResult()
    //                                              {
    //                                                  IDThangPho = item.Truong.ThanhPho.ID,
    //                                                  TinhThanh = item.Truong.ThanhPho.Ten,
    //                                                  MaTruong = item.Truong.MaTruong,
    //                                                  TenTruong = item.Truong.Ten,
    //                                                  MaNganh = item.MaNganh,
    //                                                  TenNganh = item.Nganh.Ten,
    //                                                  Diem = (float)item.Diem1,
    //                                                  IDBaiViet = 0,
    //                                                  TieuDeBaiViet = "",
    //                                                  Loai = "S",
    //                                                  linkLogo = (item.Truong.linkLogo == null ? "/Content/university.png" : item.Truong.linkLogo)
    //                                              });

    //}
}