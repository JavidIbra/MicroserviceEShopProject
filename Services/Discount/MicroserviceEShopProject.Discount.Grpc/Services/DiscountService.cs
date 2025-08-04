using Grpc.Core;
using Mapster;
using MicroserviceEShopProject.Discount.Grpc.Data;
using MicroserviceEShopProject.Discount.Grpc.Models;
using MicroserviceEShopProject.Discount.Grpc.Protos;
using Microsoft.EntityFrameworkCore;

namespace MicroserviceEShopProject.Discount.Grpc.Services
{
    public class DiscountService(DiscountContext discountContext, ILogger<DiscountService> logger)
        : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await discountContext.Coupons
                                    .FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            coupon ??= new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };

            logger.LogInformation("Discount is retrieved for Productname: {productName}",request.ProductName);

            CouponModel model = coupon.Adapt<CouponModel>();

            return model;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>()
                ?? throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request object"));

            await discountContext.Coupons.AddAsync(coupon);
            await discountContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully created. ProductName: {productName}", coupon.ProductName);

            var couponModel = coupon.Adapt<CouponModel>();

            return couponModel;
        }

        public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            return base.UpdateDiscount(request, context);
        }

        public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            return base.DeleteDiscount(request, context);
        }
    }
}
