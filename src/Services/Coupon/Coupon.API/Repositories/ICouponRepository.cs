using Coupon.API.Models;

namespace Coupon.API.Repositories

{
    public interface ICouponRepository
    {
        Task<Models.Coupon> FindCouponByCodeAsync(string code);

        Task UpdateCouponConsumedByCodeAsync(string code, int orderId);

        Task UpdateCouponReleasedByOrderIdAsync(int orderId);
    }
}
