using System.Collections.Generic;

namespace InternetAuction.BLL.Models.Lot
{
    /// <summary>
    /// Represents a lot category DTO.
    /// </summary>
    public class LotCategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<LotModel> Lots { get; set; }
    }
}