using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SauYoo
{
    //加密类
    class Pass_Deal
    {
        public string[]  encode_Str(string pass) {
            string encode_str = null;
            string keys = null;
            Random random = new Random();
            int random_i = 0;
            for (int i = 0; i < pass.Length; i++) {
                random_i = random.Next(1, 10);
                keys += random_i;
                encode_str += (random_i * (int)pass[i]).ToString().Length + (random_i*(int)pass[i]).ToString();
            }
            string[] R_Info = {encode_str, keys, };
            return R_Info;
        }

        public string decode_Str(string encode_pass, string keys) {
            string decode_str = null;
            char char_pass;
            int Str_Len = 0;
            int key;
            for (int i=0;i<keys.Length;i++) {
                Str_Len = Convert.ToInt32(encode_pass.Substring(0, 1));
                key = Convert.ToInt32(keys.Substring(i, 1));
                char_pass = (char) (Convert.ToInt32(encode_pass.Substring(1, Str_Len))/key);
                decode_str += char_pass.ToString();
                encode_pass = encode_pass.Substring(Str_Len+1);               
            }
            return decode_str.ToString();
        }
    }
}
