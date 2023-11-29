using Microsoft.EntityFrameworkCore;
using Quartz;
using Serilog;
using Shop_API.AppDbContext;
using Shop_API.Repository.IRepository;
using Shop_Models.Entities;

namespace Shop_API.Service
{
    public class VoucherStatusUpdateJob : IJob
    {
        private readonly ApplicationDbContext _context;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IServiceScopeFactory _scopeFactory;

        public VoucherStatusUpdateJob(ApplicationDbContext context, IVoucherRepository voucherRepository, IServiceScopeFactory scopeFactory)
        {
            _context = context;
            _voucherRepository = voucherRepository;
            _scopeFactory = scopeFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var voucherRepository = scope.ServiceProvider.GetRequiredService<IVoucherRepository>();

                try
                {
                    var vouchers = await GetVouchersFromDatabaseAsync(dbContext);
                    await UpdateStatusBasedOnEndDayAsync(vouchers, dbContext);

                }
                catch (Exception ex)
                {
                    Log.Error($"An error occurred in VoucherStatusUpdateJob: {ex}");
                }
            }
        }

        private async Task<List<Voucher>> GetVouchersFromDatabaseAsync(ApplicationDbContext dbContext)
        {
            var listVoucher = await dbContext.Vouchers.ToListAsync();
            return listVoucher;
        }

        private async Task UpdateStatusBasedOnEndDayAsync(List<Voucher> vouchers, ApplicationDbContext dbContext)
        {
            // Lấy thời gian hiện tại
            DateTime currentDate = DateTime.Now;

            // Duyệt qua từng voucher và cập nhật trạng thái nếu endDay đã qua
            foreach (var voucher in vouchers)
            {
                if (voucher.EndDay < currentDate /*&& voucher.Status == 1*/)
                {
                    voucher.Status = 0; // Đặt trạng thái thành "Đã kết thúc"

                    // Lưu thay đổi vào cơ sở dữ liệu
                    await SaveChangesAsync(voucher, dbContext);
                }
                if (voucher.StarDay <= currentDate && voucher.Status == 3)
                {
                    voucher.Status = 1; // Đặt trạng thái thành "Đang diễn ra "

                   
                    await SaveChangesAsync(voucher, dbContext);
                }
                if (voucher.SoLuong == 0 /*&& voucher.Status == 1*/)
                {
                    voucher.Status = 0; // Đặt trạng thái thành "Đã kết thúc "

                   
                    await SaveChangesAsync(voucher, dbContext);
                }
                if (voucher.SoLuong > 0 && voucher.EndDay >= currentDate && voucher.StarDay <= currentDate)  
                {
                    voucher.Status = 1; // Đặt trạng thái thành "Đang diễn ra "

                   
                    await SaveChangesAsync(voucher, dbContext);
                }
                if (voucher.SoLuong > 0 && voucher.EndDay > currentDate && voucher.StarDay > currentDate) 
                {
                    voucher.Status = 3; // Đặt trạng thái thành "Sắp diễn ra "

                   
                    await SaveChangesAsync(voucher, dbContext);
                }
            }
        }

        private async Task SaveChangesAsync(Voucher voucher, ApplicationDbContext dbContext)
        {
            dbContext.Vouchers.Update(voucher);
            await dbContext.SaveChangesAsync();
        }
    }
}
