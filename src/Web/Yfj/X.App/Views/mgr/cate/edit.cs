using System.Linq;
using X.Web;

namespace X.App.Views.mgr.cate
{
    public class edit : xmg
    {
        public int cid { get; set; }
        public int pid { get; set; }

        string code = "goods.cate";
        protected override string GetParmNames
        {
            get
            {
                return "cid-pid";
            }
        }
        protected override void InitDict()
        {
            base.InitDict();
            if (cid > 0)
            {
                var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == cid);
                if (ent == null) throw new XExcep("0x0005");
                dict.Add("item", ent);
                var up = DB.x_dict.FirstOrDefault(o => o.code == code && o.value == ent.upval.Split('-').Last());
                if (up == null) dict.Add("up", "0|无");
                else dict.Add("up", up.value + "|" + up.name);
            }
            else if (pid > 0)
            {
                var ent = DB.x_dict.FirstOrDefault(o => o.dict_id == pid);
                if (ent != null) dict.Add("up", ent.value + "|" + ent.name);
            }
            else
            {
                dict.Add("up", "0|无");
            }
        }
    }
}
