using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    /// <summary>
    /// 竞赛进行状态
    /// </summary>
    public enum MatchState
    {
        #region 单项理论赛

        /// <summary>
        /// 单项理论赛-未开始=100
        /// </summary>
        DX_WKS = 100,
        /// <summary>
        /// 单项理论赛-报名中=101
        /// </summary>
        DX_BMZ = 101,
        /// <summary>
        /// 单项理论赛-待比赛=102
        /// </summary>
        DX_DBS = 102,
        /// <summary>
        /// 单项理论赛-比赛中=103
        /// </summary>
        DX_BSZ = 103,
        /// <summary>
        /// 单项理论赛-成绩待发布=104
        /// </summary>
        DX_CJDFB = 104,
        /// <summary>
        /// 单项理论赛-比赛结束=105
        /// </summary>
        DX_BSJS = 105,

        #endregion

        #region 单项实训赛

        /// <summary>
        /// 单项实训赛-未开始=200
        /// </summary>
        SX_WKS = 200,
        /// <summary>
        /// 单项实训赛-报名中=201
        /// </summary>
        SX_BMZ = 201,
        /// <summary>
        /// 单项实训赛-待比赛=202
        /// </summary>
        SX_DBS = 202,
        /// <summary>
        /// 单项实训赛-比赛中=203
        /// </summary>
        SX_BSZ = 203,
        /// <summary>
        /// 单项实训赛-待评分=204
        /// </summary>
        SX_DPF = 204,
        /// <summary>
        /// 单项实训赛-评分中=205
        /// </summary>
        SX_PFZ = 205,
        /// <summary>
        /// 单项实训赛-成绩待发布=206
        /// </summary>
        SX_CJDFB = 206,
        /// <summary>
        /// 单项实训赛-比赛结束=207
        /// </summary>
        SX_BSJS = 207,

        #endregion

        #region 复合赛

        /// <summary>
        /// 复合赛-未开始=300
        /// </summary>
        HF_WKS = 300,
        /// <summary>
        /// 复合赛-报名中=301
        /// </summary>
        HF_BMZ = 301,
        /// <summary>
        /// 复合赛-待初赛=302
        /// </summary>
        HF_DCS = 302,
        /// <summary>
        /// 复合赛-初赛中=303
        /// </summary>
        HF_CSZ = 303,
        /// <summary>
        /// 复合赛-初赛成绩待发布=304
        /// </summary>
        HF_CSCJDFB = 304,
        /// <summary>
        /// 复合赛-待复赛=305
        /// </summary>
        HF_DFS = 305,
        /// <summary>
        /// 复合赛-复赛中=306
        /// </summary>
        HF_FSZ = 306,
        /// <summary>
        /// 复合赛-待评分=307
        /// </summary>
        HF_DPF = 307,
        /// <summary>
        /// 复合赛-评分中=308
        /// </summary>
        HF_PFZ = 308,
        /// <summary>
        /// 复合赛-成绩待发布=309
        /// </summary>
        HF_CJDFB = 309,
        /// <summary>
        /// 复合赛-比赛结束=310
        /// </summary>
        HF_BSJS = 310

        #endregion

    }
}
