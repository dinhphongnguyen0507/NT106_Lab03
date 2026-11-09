using System;
using System.Collections.Generic;
using System.Text;

namespace Bai05
{
    public record GoiTin(
    string Loai,
    long idMon = 0,
    string? IDND = null,
    string? TenMon = null,
    string? TenNguoi = null,
    string? ThongBao = null);
   }
