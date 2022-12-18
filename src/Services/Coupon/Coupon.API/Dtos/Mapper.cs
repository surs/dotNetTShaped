namespace Coupon.API.Dtos
{
    public class Mapper : IMapper<CouponDto, Models.Coupon>
    {
        public CouponDto Translate(Models.Coupon entity)
        {
            return new CouponDto
            {
                Code = entity.Code,
                Discount = entity.Discount
            };
        }
    }
}
