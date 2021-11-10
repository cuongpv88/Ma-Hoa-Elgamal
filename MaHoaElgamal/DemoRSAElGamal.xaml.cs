using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Collections;



namespace RSA_ELGAMAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DemoRSAElGamal : Window
    {
        public DemoRSAElGamal()
        {
            InitializeComponent();
            rd_td.IsChecked = true;
            btMaHoa.IsEnabled = false;
            btGiaiMa.IsEnabled = false;
            //rd_tdRSA.IsChecked = true;
            //rd_tcRSA.IsChecked = false;
            //rsa_maHoaBanRoMoi.IsEnabled = false;            

        }

        #region code mã hóa elgamal
        private void rd_tc_Checked(object sender, RoutedEventArgs e)
        {
            So_A.IsEnabled = So_D.IsEnabled = So_K.IsEnabled = So_X.IsEnabled = So_Y.IsEnabled = false;
            So_P.IsEnabled = true;
            bt_taoKhoa.Content = "Tạo khóa";
            reset();
            bt_taoKhoa.IsEnabled = true;
            //So_A.Text = So_D.Text = so_K.Text = So_P.Text = So_X.Text = so_Y.Text = string.Empty;
        }

        private void rd_td_Checked(object sender, RoutedEventArgs e)
        {
            So_A.IsEnabled = So_D.IsEnabled = So_K.IsEnabled = So_P.IsEnabled = So_X.IsEnabled = So_Y.IsEnabled = false;
            reset();
            bt_taoKhoa.Content = "Tạo khóa";
            bt_taoKhoa.IsEnabled = true;
            // So_A.Text = So_D.Text = so_K.Text = So_P.Text = So_X.Text = so_Y.Text = string.Empty;
        }

        private void bt_taoKhoamoi_Click(object sender, RoutedEventArgs e)
        {
            reset();
            // So_A.Text = So_D.Text = so_K.Text = So_P.Text = So_X.Text = so_Y.Text = string.Empty;
        }

        private void reset()
        {
            So_A.Text = So_D.Text = So_K.Text = So_P.Text = So_X.Text = So_Y.Text = string.Empty;
        }
        

        private bool IsNumber(string Text_x)
        {
            int outPut;
            return int.TryParse(Text_x, out outPut);
        }
        

        private void rsa_soP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "." && IsNumber(e.Text) == false)
            {
                e.Handled = true;
            }
            else if (e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    e.Handled = true;
                }
            }
        }
        

        public int EsoP, EsoQ, E_So_G_A, EsoA, EsoX, EsoD, EsoK, EsoY;
        public int danhDau = 0;
        private int E_ChonSoNgauNhien()
        {
            Random rdE = new Random();
            return rdE.Next(1000, 1500);// tốc độ chậm nên chọn số bé
        }
        private bool E_kiemTraNguyenTo(int so_kt)
        {
            bool kiemtra = true;
            if (so_kt == 2 || so_kt == 3)
            {
                // kiemtra = true;
                return kiemtra;
            }
            else
            {
                if (so_kt == 1 || so_kt % 2 == 0 || so_kt % 3 == 0)
                {
                    kiemtra = false;
                }
                else
                {
                    for (int i = 5; i <= Math.Sqrt(so_kt); i = i + 6)
                        if (so_kt % i == 0 || so_kt % (i + 2) == 0)
                        {
                            kiemtra = false;
                            break;
                        }
                }
            }
            return kiemtra;
        }  //"Hàm kiểm tra nguyên tố"
        private bool E_kiemTraUocCuaSoP(int so_P, int so_Q)
        {
            bool kt_Okie = true;
            if ((so_P - 1) % so_Q == 0)
            {
                kt_Okie = true;
            }
            else
                kt_Okie = false;
            return kt_Okie;
        }
        private bool E_kiemTraPTSinh(int so_kt, int E_SoP_, int E_soQ_)// kiem tra phan tu sinh
        {
            bool ktOkie = true;
            int soMu = E_SoP_ - 1 / E_soQ_;
            int ketQuaKT = E_LuyThuaModulo_(so_kt, soMu, E_SoP_);

            if (ketQuaKT != 1)
            {
                ktOkie = true;
            }
            else
            {
                if (ketQuaKT == 1) ktOkie = false;
            }
            return ktOkie;
        }
        private bool nguyenToCungNhau(int ai, int bi)// "Hàm kiểm tra hai số nguyên tố cùng nhau"
        {
            bool ktx_;
            // giải thuật Euclid;
            int temp;
            while (bi != 0)
            {
                temp = ai % bi;
                ai = bi;
                bi = temp;
            }
            if (ai == 1) { ktx_ = true; }
            else ktx_ = false;
            return ktx_;
        }
        private string TapP_1(int soDauVao)
        {
            string ChuoiDauRa = string.Empty;
            for (int i = 1; i < soDauVao; i++)
            {
                if (nguyenToCungNhau(soDauVao, i) == true)
                { ChuoiDauRa += i.ToString() + "#"; }
            }
            return ChuoiDauRa;
        }
        // Find the all factors of Ø  {f1,f2,….,fn} – { 1 }
        private string Tap_Qi(int soDauvao) // tìm các số khi phân tích ra thừa số của số P
        {
            string ChuoiDauRa = string.Empty;
            int soix = 2;
            while (soDauvao != 1)
            {
                if (soDauvao % soix == 0)
                {
                    ChuoiDauRa += soix.ToString() + "#";
                    soDauvao = soDauvao / soix;
                }
                else soix++;
            }
            return ChuoiDauRa;
        }
        public int E_LuyThuaModulo_(int CoSo_, int SoMu_, int soModulo_)
        {

            //Sử dụng thuật toán "bình phương nhân"
            //Chuyển e sang hệ nhị phân
            int[] a = new int[100];
            int k = 0;
            do
            {
                a[k] = SoMu_ % 2;
                k++;
                SoMu_ = SoMu_ / 2;
            }
            while (SoMu_ != 0);
            //Quá trình lấy dư
            int kq = 1;

            for (int i = k - 1; i >= 0; i--)
            {
                kq = (kq * kq) % soModulo_;
                if (a[i] == 1)
                    kq = (kq * CoSo_) % soModulo_;
            }
            return kq;
        }

        //Ví dụ: x=  y2(y1a )-1 mod  p  = 133.(394^109)^-1 mod 569  =257
        private int E_tinhModulo_nghichdao(int SoNCNDn, int SoMdlm)
        {
            int kd = SoMdlm;
            int r = 1, q, y0 = 0, y1 = 1, y = 0;
            while (SoNCNDn != 0)
            {
                r = SoMdlm % SoNCNDn;
                if (r == 0)
                    break;
                else
                {
                    q = SoMdlm / SoNCNDn;
                    y = y0 - y1 * q;
                    SoMdlm = SoNCNDn;
                    SoNCNDn = r;
                    y0 = y1;
                    y1 = y;
                }
            }
            if (y >= 0)
                return y;
            else
            {
                y = kd + y;
                return y;
            }
        }
        private int E_TinhC1muxModP(int SoC1, int SomuX, int somDLP)
        {
            int kq_E_TinhC1muxModP = 1;
            for (int i = 0; i <= SomuX; i++)
            {
                kq_E_TinhC1muxModP = kq_E_TinhC1muxModP * E_tinhModulo_nghichdao(SoC1, somDLP);
            }
            return kq_E_TinhC1muxModP;
        }
        private void TaoKhoa_click()
        {
            EsoQ = E_So_G_A = EsoA = EsoX = EsoD = EsoK = 0;

            // chọn số nguyên tố ngẫu nhiên Q thỏa mãn Q là ước của P - 1;
            do
            {
                Random rdQ = new Random();
                EsoQ = rdQ.Next(2, EsoP - 1);
            }
            while (!E_kiemTraNguyenTo(EsoP) || !E_kiemTraUocCuaSoP(EsoP, EsoQ));
            // tìm số G để tìm số A (A là phần tử sinh): 
            do
            {
                Random rdE_So_G_A = new Random();
                E_So_G_A = rdE_So_G_A.Next(2, EsoP - 1);
            }
            while (!E_kiemTraPTSinh(E_So_G_A, EsoP, EsoQ));

            EsoA = E_LuyThuaModulo_(E_So_G_A, EsoP - 1 / EsoQ, EsoP); // phần tử sinh

            do
            {
                Random rdEsoX = new Random();
                EsoX = rdEsoX.Next(2, EsoP - 2);
            }
            while (EsoX == EsoQ || EsoX == E_So_G_A);
            // d= a^x mod P
            EsoD = E_LuyThuaModulo_(EsoA, EsoX, EsoP);// beta; d          
            do
            {
                Random rdEsoK = new Random();
                EsoK = rdEsoK.Next(2, EsoP - 2);
            }
            while (EsoK == EsoX || EsoK == EsoA || EsoK == EsoQ || EsoK == E_So_G_A || !nguyenToCungNhau(EsoK, EsoP - 1));
            // Tính Y = A^k mod p - Khóa công khai
            EsoY = E_LuyThuaModulo_(EsoA, EsoK, EsoP);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".txt";
            ofd.Filter = "Text Document (.txt)|*txt";
            if(ofd.ShowDialog()==true)
            {
                string filename = ofd.FileName;
                txtBanRo.Text = File.ReadAllText(filename);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        
        private void bt_taoKhoa_Click(object sender, RoutedEventArgs e)
        {
            if (rd_td.IsChecked == true && rd_tc.IsChecked == false)
            {
                // thực hiện thao tác tạo khóa ngẫu nhiên 
                reset();

                // chọn số nguyên tố ngẫu nhiên P
                EsoP = 0;
                do
                {
                    EsoP = E_ChonSoNgauNhien();
                }
                while (E_kiemTraNguyenTo(EsoP) == false);

                TaoKhoa_click();
                So_P.Text = EsoP.ToString();
                So_A.Text = EsoA.ToString();
                So_X.Text = EsoX.ToString();
                So_D.Text = EsoD.ToString();
                So_K.Text = EsoK.ToString();
                So_Y.Text = EsoY.ToString();
                bt_taoKhoa.Content = "Tạo khóa";

            }
            else
            {
                if (rd_td.IsChecked == false && rd_tc.IsChecked == true)//(rd_tudongchon_.Checked == false && rd_tuychon_.Checked == true)
                {
                    // thực hiện thao tác tạo khóa tùy chọn 
                    if (So_P.Text == "")
                    {
                        MessageBox.Show("Phải chọn số P ", "Thông Báo ", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        EsoP = int.Parse(So_P.Text);
                        if (E_kiemTraNguyenTo(EsoP) == false)
                        {
                            MessageBox.Show("Phải chọn P là số nguyên tố ", "Thông Báo ", MessageBoxButton.OK, MessageBoxImage.Error);
                            So_P.Focus();
                        }
                        else
                            if (EsoP < 1000)
                            {
                                MessageBox.Show("Số P quá nhỏ! Nhập số khác ", "Thông Báo ", MessageBoxButton.OK, MessageBoxImage.Error);
                                So_P.Focus();
                            }
                            else
                            {
                                TaoKhoa_click();
                                So_P.Text = EsoP.ToString();
                                So_A.Text = EsoA.ToString();
                                So_X.Text = EsoX.ToString();
                                So_D.Text = EsoD.ToString();
                                So_K.Text = EsoK.ToString();
                                So_Y.Text = EsoY.ToString();
                                bt_taoKhoa.IsEnabled = false;
                            //bt_taokhoaTuychonMoi.Visible = true;
                        }
                    }
                }
            }
            danhDau = 1;
            btMaHoa.IsEnabled = true;
        }

        public string E_MaHoa(string ChuoiVao)
        {
            //Chuyen xau thanh ma Unicode         

            byte[] mhE_temp1 = Encoding.Unicode.GetBytes(ChuoiVao);
            string base64 = Convert.ToBase64String(mhE_temp1);

            // Chuyển xâu thành mã Unicode dạng số          
            int[] mh_temp2 = new int[base64.Length];
            for (int i = 0; i < base64.Length; i++)
            {
                mh_temp2[i] = (int)base64[i];
                //txtm1.Text += mh_temp2[i].ToString() + "#";
            }

            //txt_ChuoimaBanRo.Text = chuoi(mh_temp2);            
            //Mảng a chứa các kí tự sẽ  mã hóa
            int[] mh_temp3 = new int[mh_temp2.Length];
            // thực hiện mã hóa: z = (d^k * m ) mod p

            for (int i = 0; i < mh_temp2.Length; i++)
            {
                mh_temp3[i] = ((mh_temp2[i] % EsoP) * (E_LuyThuaModulo_(EsoD, EsoK, EsoP))) % EsoP;
                //txtm2.Text += mh_temp3[i].ToString()+"#";
            }
            //Chuyển sang kiểu kí tự trong bảng mã Unicode
            string str = "";
            for (int i = 0; i < mh_temp3.Length; i++)
            {
                str = str + (char)mh_temp3[i];
                // txtm3.Text = (char)mh_temp3[i] + "#";
            }
            byte[] E_data1 = Encoding.Unicode.GetBytes(str);
            string BanMaHoa = Convert.ToBase64String(E_data1);
            return BanMaHoa;

        }
        public string E_GiaiMa(string ChuoiVao)
        {
            //Chuyen chuoi thanh ma Unicode       
            string BanGiaiMa = "";

            byte[] Egm_temp1 = Convert.FromBase64String(ChuoiVao);
            string Egm_giaima = Encoding.Unicode.GetString(Egm_temp1);

            int[] Eb = new int[Egm_giaima.Length];
            for (int i = 0; i < Egm_giaima.Length; i++)
            {
                Eb[i] = (int)Egm_giaima[i];

            }
            //Giải mã
            //   m = ( r * z ) mod p =((r mod p) * (z mod p))mod p  with r = y^(p-1-x) mod p

            int[] Ec = new int[Eb.Length];
            int sor = E_LuyThuaModulo_(EsoY, (EsoP - (1 + EsoX)), EsoP);
            //txtm7.Text = sor.ToString();
            for (int i = 0; i < Ec.Length; i++)
            {
                Ec[i] = (Eb[i] * sor) % EsoP;// giải mã

            }
            string str = "";
            for (int i = 0; i < Ec.Length; i++)
            {
                str = str + (char)Ec[i];
            }
            byte[] data2 = Convert.FromBase64String(str);
            BanGiaiMa = Encoding.Unicode.GetString(data2);
            return BanGiaiMa;
        }

        private void btMaHoa_Click(object sender, RoutedEventArgs e)
        {
            if (danhDau != 1)
            {
                MessageBox.Show("Bạn chưa chọn khóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (txtBanRo.Text == string.Empty)
                {
                    MessageBox.Show("Bạn chưa nhập chuỗi cần mã hóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (danhDau == 1)
                    {
                        txt_maHoaBanRo.Text = string.Empty;

                        string Egm_chuoiMaHoa = E_MaHoa(txtBanRo.Text);
                        txt_maHoaBanRo.Text = Egm_chuoiMaHoa;
                        txt_banMaHoaNhanDuoc.Text = Egm_chuoiMaHoa;
                        danhDau = 2;
                        btMaHoa.IsEnabled = false;
                        btGiaiMa.IsEnabled = true;
                    }
                }
            }
        }

        private void btGiaiMa_Click(object sender, RoutedEventArgs e)
        {

            if (danhDau != 2)
            {
                MessageBox.Show("Bạn chưa chọn tệp giải mã!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (danhDau == 2)
            {
                txt_banGiaima.Text = string.Empty;
                txt_banGiaima.Text = E_GiaiMa(txt_banMaHoaNhanDuoc.Text);
            }
            danhDau = 1;
            btMaHoa.IsEnabled = false;
            btGiaiMa.IsEnabled = false;
        }

        private void btThoat_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btTaoBanRoMoi_Click(object sender, RoutedEventArgs e)
        {
            txtBanRo.Text = txt_maHoaBanRo.Text = txt_banMaHoaNhanDuoc.Text = txt_banGiaima.Text = string.Empty;
            btMaHoa.IsEnabled = true;
        }

        #endregion
        //#region Code mã hóa RSA
        //private void reset_rsa()
        //{
        //    rsa_soP.Text = rsa_soQ.Text = rsa_soPhiN.Text = rsa_soN.Text = rsa_soE.Text = rsa_soD.Text = string.Empty;

        //}

        //int RSA_soP, RSA_soQ, RSA_soN, RSA_soE, RSA_soD, RSA_soPhi_n;
        //public int RSA_d_dau = 0;
        //private int RSA_ChonSoNgauNhien()
        //{
        //    Random rd = new Random();
        //    return rd.Next(11, 101);// tốc độ chậm nên chọn số bé
        //}
        ////"Hàm kiểm tra nguyên tố"
        //private bool RSA_kiemTraNguyenTo(int xi)
        //{
        //    bool kiemtra = true;
        //    if (xi == 2 || xi == 3)
        //    {
        //        // kiemtra = true;
        //        return kiemtra;
        //    }
        //    else
        //    {
        //        if (xi == 1 || xi % 2 == 0 || xi % 3 == 0)
        //        {
        //            kiemtra = false;
        //        }
        //        else
        //        {
        //            for (int i = 5; i <= Math.Sqrt(xi); i = i + 6)
        //                if (xi % i == 0 || xi % (i + 2) == 0)
        //                {
        //                    kiemtra = false;
        //                    break;
        //                }
        //        }
        //    }
        //    return kiemtra;
        //}
        //// "Hàm kiểm tra hai số nguyên tố cùng nhau"
        //private bool RSA_nguyenToCungNhau(int ai, int bi)
        //{
        //    bool ktx_;
        //    // giải thuật Euclid;
        //    int temp;
        //    while (bi != 0)
        //    {
        //        temp = ai % bi;
        //        ai = bi;
        //        bi = temp;
        //    }
        //    if (ai == 1) { ktx_ = true; }
        //    else ktx_ = false;
        //    return ktx_;
        //}
        //// "Hàm lấy mod"
        //public int RSA_mod(int mx, int ex, int nx)
        //{

        //    //Sử dụng thuật toán "bình phương nhân"
        //    //Chuyển e sang hệ nhị phân
        //    int[] a = new int[100];
        //    int k = 0;
        //    do
        //    {
        //        a[k] = ex % 2;
        //        k++;
        //        ex = ex / 2;
        //    }
        //    while (ex != 0);
        //    //Quá trình lấy dư
        //    int kq = 1;
        //    for (int i = k - 1; i >= 0; i--)
        //    {
        //        kq = (kq * kq) % nx;
        //        if (a[i] == 1)
        //            kq = (kq * mx) % nx;
        //    }
        //    return kq;
        //}

        //private void RSA_taoKhoa()
        //{
        //    //Tinh n=p*q
        //    RSA_soN = RSA_soP * RSA_soQ;
        //    rsa_soN.Text = RSA_soN.ToString();
        //    //Tính Phi(n)=(p-1)*(q-1)
        //    RSA_soPhi_n = (RSA_soP - 1) * (RSA_soQ - 1);
        //    rsa_soPhiN.Text = RSA_soPhi_n.ToString();
        //    //Tính e là một số ngẫu nhiên có giá trị 0< e <phi(n) và là số nguyên tố cùng nhau với Phi(n)
        //    do
        //    {
        //        Random RSA_rd = new Random();
        //        RSA_soE = RSA_rd.Next(2, RSA_soPhi_n);
        //    }
        //    while (!nguyenToCungNhau(RSA_soE, RSA_soPhi_n));
        //    rsa_soE.Text = RSA_soE.ToString();

        //    //Tính d là nghịch đảo modular của e
        //    RSA_soD = 0;
        //    int i = 2;
        //    while (((1 + i * RSA_soPhi_n) % RSA_soE) != 0 || RSA_soD <= 0)
        //    {
        //        i++;
        //        RSA_soD = (1 + i * RSA_soPhi_n) / RSA_soE;
        //    }
        //    rsa_soD.Text = RSA_soD.ToString();
        //}
        //public void RSA_MaHoa(string ChuoiVao)
        //{
        //    // taoKhoa();
        //    // Chuyen xau thanh ma Unicode
        //    byte[] mh_temp1 = Encoding.Unicode.GetBytes(ChuoiVao);
        //    string base64 = Convert.ToBase64String(mh_temp1);

        //    // Chuyen xau thanh ma Unicode
        //    int[] mh_temp2 = new int[base64.Length];
        //    for (int i = 0; i < base64.Length; i++)
        //    {
        //        mh_temp2[i] = (int)base64[i];
        //    }

        //    //Mảng a chứa các kí tự đã mã hóa
        //    int[] mh_temp3 = new int[mh_temp2.Length];
        //    for (int i = 0; i < mh_temp2.Length; i++)
        //    {
        //        mh_temp3[i] = RSA_mod(mh_temp2[i], RSA_soE, RSA_soN); // mã hóa
        //    }

        //    //Chuyển sang kiểu kí tự trong bảng mã Unicode
        //    string str = "";
        //    for (int i = 0; i < mh_temp3.Length; i++)
        //    {
        //        str = str + (char)mh_temp3[i];
        //    }
        //    byte[] data = Encoding.Unicode.GetBytes(str);
        //    rsa_BanMaHoa.Text = Convert.ToBase64String(data);
        //    rsa_banMaHoaGuiDen.Text = Convert.ToBase64String(data);

        //}
        //// hàm giải mã
        //public void RSA_GiaiMa(string ChuoiVao)
        //{
        //    byte[] temp2 = Convert.FromBase64String(ChuoiVao);
        //    string giaima = Encoding.Unicode.GetString(temp2);

        //    int[] b = new int[giaima.Length];
        //    for (int i = 0; i < giaima.Length; i++)
        //    {
        //        b[i] = (int)giaima[i];
        //    }
        //    //Giải mã
        //    int[] c = new int[b.Length];
        //    for (int i = 0; i < c.Length; i++)
        //    {
        //        c[i] = RSA_mod(b[i], RSA_soD, RSA_soN);// giải mã
        //    }

        //    string str = "";
        //    for (int i = 0; i < c.Length; i++)
        //    {
        //        str = str + (char)c[i];
        //    }
        //    byte[] data2 = Convert.FromBase64String(str);
        //    rsa_banGiaiMa.Text = Encoding.Unicode.GetString(data2);

        //}
        //private void rsa_TaoKhoa_Click(object sender, RoutedEventArgs e)
        //{

        //    if (rd_tdRSA.IsChecked == true && rd_tcRSA.IsChecked == false)
        //    {
        //        reset_rsa();
        //        RSA_soP = RSA_soQ = 0;
        //        do
        //        {
        //            RSA_soP = RSA_ChonSoNgauNhien();
        //            RSA_soQ = RSA_ChonSoNgauNhien();
        //        }
        //        while (RSA_soP == RSA_soQ || !RSA_kiemTraNguyenTo(RSA_soP) || !RSA_kiemTraNguyenTo(RSA_soQ));
        //        rsa_soP.Text = RSA_soP.ToString();
        //        rsa_soQ.Text = RSA_soQ.ToString();
        //        RSA_taoKhoa();
        //        RSA_d_dau = 1;
        //        rsa_TaoKhoa.Content = "Tạo lại khóa mới";
        //        rsa_TaoKhoa.IsEnabled = false;
        //        rd_tcRSA.IsEnabled = false;
        //        rd_tdRSA.IsEnabled = false;
        //        rsa_btMaHoa.IsEnabled = true;
        //    }
        //    else
        //    {
        //        if (rd_tdRSA.IsChecked == false && rd_tcRSA.IsChecked == true)
        //        {
        //            if (rsa_soP.Text == "" || rsa_soQ.Text == "")
        //                MessageBox.Show("Phải nhập đủ 2 số ", "Thông Báo ", MessageBoxButton.OK, MessageBoxImage.Error);
        //            else
        //            {
        //                RSA_soP = int.Parse(rsa_soP.Text);
        //                RSA_soQ = int.Parse(rsa_soQ.Text);
        //                if (RSA_soP == RSA_soQ)
        //                {
        //                    MessageBox.Show("Nhập 2 số nguyên tố khác nhau ", " Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
        //                    rsa_soQ.Focus();
        //                }
        //                else
        //                {
        //                    if (!RSA_kiemTraNguyenTo(RSA_soP) || RSA_soP <= 1)
        //                    {
        //                        MessageBox.Show("Phải nhập số nguyên  tố [p] lớn hơn 1 ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
        //                        rsa_soP.Focus();
        //                    }
        //                    else
        //                    {
        //                        if (!RSA_kiemTraNguyenTo(RSA_soQ) || RSA_soQ <= 1)
        //                        {
        //                            MessageBox.Show("Phải nhập số nguyên  tố [q] lớn hơn 1 ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Error);
        //                            rsa_soQ.Focus();
        //                        }
        //                        else
        //                        {
        //                            RSA_taoKhoa();
        //                            rsa_soP.Text = RSA_soP.ToString();
        //                            rsa_soQ.Text = RSA_soQ.ToString();
        //                            RSA_d_dau = 1;
        //                            //bt_taokhoaTuychonMoi.Visible = true;
        //                            rsa_TaoKhoa.IsEnabled = false;
        //                        }
        //                    }
        //                }
        //            }

        //        }
        //    }
        //}

        //private void rsa_btMaHoa_Click(object sender, RoutedEventArgs e)
        //{
        //    if (RSA_d_dau != 1)
        //    { MessageBox.Show("Bạn chưa tạo khóa!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information); }

        //    else
        //    {
        //        if (rsa_BanRo.Text == "")
        //        {
        //            MessageBox.Show("Bạn chưa nhập bản rõ để mã hóa!", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }

        //        else
        //        {
        //            // thực hiện mã hóa
        //            try
        //            {
        //                RSA_MaHoa(rsa_BanRo.Text);
        //                rsa_btMaHoa.IsEnabled = false;
        //                rsa_btGiaiMa.IsEnabled = true;
        //                RSA_d_dau = 2;
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        //            }
        //        }
        //    }
        //}

        //private void rsa_btGiaiMa_Click(object sender, RoutedEventArgs e)
        //{

        //    if (RSA_d_dau != 2)
        //        MessageBox.Show("Bạn phải tạo khóa trước ", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
        //    else
        //        try
        //        {
        //            RSA_GiaiMa(rsa_BanMaHoa.Text);

        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    rsa_btGiaiMa.IsEnabled = false;
        //    RSA_d_dau = 1;
        //    rsa_maHoaBanRoMoi.IsEnabled = true;
        //}


        //private void rd_tdRSA_Checked(object sender, RoutedEventArgs e)
        //{
        //    rsa_TaoKhoa.IsEnabled = true;
        //    rsa_soP.Text = rsa_soQ.Text = rsa_soPhiN.Text = rsa_soN.Text = rsa_soE.Text = rsa_soD.Text = string.Empty;
        //    rsa_soP.IsEnabled = rsa_soQ.IsEnabled = rsa_soPhiN.IsEnabled = rsa_soN.IsEnabled = rsa_soE.IsEnabled = rsa_soD.IsEnabled = false;

        //}

        //private void rd_tcRSA_Checked(object sender, RoutedEventArgs e)
        //{
        //    rsa_TaoKhoa.IsEnabled = true;
        //    rsa_soP.Text = rsa_soQ.Text = rsa_soPhiN.Text = rsa_soN.Text = rsa_soE.Text = rsa_soD.Text = string.Empty;
        //    rsa_soP.IsEnabled = rsa_soQ.IsEnabled = rsa_soPhiN.IsEnabled = rsa_soN.IsEnabled = rsa_soE.IsEnabled = rsa_soD.IsEnabled = true;
        //}


        //private void rsa_maHoaBanRoMoi_Click(object sender, RoutedEventArgs e)
        //{
        //    rsa_btMaHoa.IsEnabled = true;
        //    rsa_BanRo.Text = rsa_BanMaHoa.Text = rsa_banMaHoaGuiDen.Text = rsa_banGiaiMa.Text = string.Empty;
        //    RSA_d_dau = 1;
        //    rsa_maHoaBanRoMoi.IsEnabled = false;
        //}

        //private void rsa_btThoat_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //private void rsa_TaoKhoaMoi_Click(object sender, RoutedEventArgs e)
        //{
        //    rsa_maHoaBanRoMoi.IsEnabled = false;
        //    RSA_d_dau = 0;
        //    rsa_TaoKhoa.IsEnabled = true;
        //    rd_tdRSA.IsEnabled = true;
        //    rd_tdRSA.IsChecked = true;
        //    rd_tcRSA.IsEnabled = true;
        //    rd_tcRSA.IsChecked = false;
        //    rsa_soP.Text = rsa_soQ.Text = rsa_soPhiN.Text = rsa_soN.Text = rsa_soE.Text = rsa_soD.Text = string.Empty;

        //    rsa_banGiaiMa.Text = rsa_BanMaHoa.Text = rsa_BanRo.Text = rsa_banMaHoaGuiDen.Text = string.Empty;
        //    rsa_btGiaiMa.IsEnabled = false; rsa_btMaHoa.IsEnabled = false;

        //}

        //private void rsa_soP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    if (e.Text != "." && IsNumber(e.Text) == false)
        //    {
        //        e.Handled = true;
        //    }
        //    else if (e.Text == ".")
        //    {
        //        if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
        //        {
        //            e.Handled = true;
        //        }
        //    }
        //}
        //private bool IsNumber(string Text_x)
        //{
        //    int outPut;
        //    return int.TryParse(Text_x, out outPut);
        //}
        //#endregion
       
    }
}
