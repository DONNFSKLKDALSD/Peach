﻿

namespace Peach.Model.Models
{

    public class VodListModel
    {
        public List<VodModel> List { get; set; }

    }


    public class SmallVodListModel
    {
        public SmallVodListModel() { }
        public int limit { get; set; }
        public int page { get; set; }
        public int pagecount { get; set; }
        public int total { get; set; }

        public List<SmallVodModel> List { get; set; }

    }


    /// <summary>
    /// 简版视频
    /// </summary>
    public class SmallVodModel
    {
        public string vod_id { get; set; }
        public string vod_name { get; set; }
        public string vod_content { get; set; }
        public string? vod_pic { get; set; }
        public string? vod_remarks { get; set; }

    }


    /// <summary>
    /// 视频详情
    /// </summary>
    public class VodModel : SmallVodModel
    {
        public string? TypeId { get; set; }
        public string? typeName { get; set; }//": "剧情片美国2012",

        public string? vod_actor { get; set; }//": "贝拉·索恩 斯戴芬妮·斯考特 尼克·罗宾森 Mar",
        public string? vod_area { get; set; }//": "",
        public string? vod_director { get; set; }//": "戴斯·冯·施勒·梅耶",
        public string? vod_play_from { get; set; }//": "非凡",
        public string? vod_play_url { get; set; }//": "HD中字$http://www.8kvod.com/p/121513-1-1/",
        public string? vod_year { get; set; }//": ""

    }


    public class PlayModel
    {
        public int parse { get; set; }
        public string? jx { get; set; }
        public string? url { get; set; }
        public string? js { get; set; }
        public object header { get; set; }
        public string? parse_extra { get; set; }
        public string? isVideo { get; set; }
        public string? adRemove { get; set; }
    }


    public class LineModel
    {
        public string LineName { get; set; }

        public List<JIShuModel> Episodes { get; set; }
    }

    public class JIShuModel
    {
        public JIShuModel() { }
        public JIShuModel(string ji, string u)
        {
            JiShu = ji;
            Url = u;
        }
        public string JiShu { get; set; }
        public string Url { get; set; }
    }

}
