function GetHistoryCompetitionNum(){for(var r,t=parseInt($("#hideLSDS").val()),u=JSON.stringify(t),i=FormatNum(t,6),n=0;n<i.length;n++)r=i[n],$(".d_wrap span").eq(n).animate({top:-24*r},1e3)}function GetTotalUserNum(){for(var i,r=parseInt($("#hideZRS").val()),u=JSON.stringify(r),t=FormatNum(u,6),n=0;n<t.length;n++)i=t[n],$(".d_wrap1 span").eq(n).animate({top:-24*i},1e3)}function GetRegisterNotAduitNum(){$.ajax({url:"/CompetitionUser/Home/GetWaitJoinNum",type:"POST",success:function(n){$(".d_home_2_1 span").html(n.Data)}})}function GetSiginupNotAduitNum(){$.ajax({url:"/CompetitionUser/Home/GetHasJoinNum",type:"POST",success:function(n){$(".d_home_2_1s span").html(n.Data)}})}function GetIndexMatchList(){pageHelper.Init({url:"/CompetitionUser/Home/GetLatestCompetitionList",type:"POST",pageDiv:"#pages",bind:function(n){var t="";$(n).each(function(n,i){var r="";r+="<tr>";r+="<td>{0}<\/td>";r+="<td>{1}<\/td>";r+="<td>{2}<\/td>";r+="<td>{3}<\/td>";r+="<td>{4}<\/td>";r+="<td>";r+='<a href="/CompetitionUser/Match/Detail/{5}">进入大赛<\/a>';r+="<\/td>";r+="<\/tr>";t+=i.Type!=3?StringHelper.FormatStr(r,n+1,i.Name,i.AddUserName,i._PreliminaryStartTime,i._PreliminaryEndTime,i.Id):StringHelper.FormatStr(r,n+1,i.Name,i.AddUserName,i._PreliminaryStartTime,i._RematchEndTime,i.Id)});$("#CompetitionList").html(t)}})}function NoticePage(){$("#addNotice h3").text("发布公告");$("#cmbNotice").attr("disabled",!1);$("#addNotice .pop-button").show();$("#txtNoticeContext").val("");$("#txtNoticeContext").attr("readonly",!1);$("#cmbNotice").removeAttr("disabled");dialogHelper.Show("addNotice",500)}function GetNotice(){pageHelper.Init2({url:"/CompetitionUser/Home/GetNoticeList",type:"POST",pageDiv:"#NoticePages",bind:function(n){var t="";n.Data.length>0?($(n.Data).each(function(i,r){var u="";u+="<tr>";u+='<td name="dataNo">{0}<\/td>';u+="<td class='left'><div  title=\"{1}\">【{3}】{2}<\/div><\/td>";u+='<td><a  class="chakan" onclick="DetailsNotice(\''+r.Id+"')\">查看<\/a> ";$("#hdCreate").val()==1&&(u+='<a class="shanchu" onclick="DeleteNotice(\''+r.Id+"')\">删除<\/a>");u+="<\/td><\/tr>";t+=StringHelper.FormatStr(u,(n.PageIndex-1)*n.PageSize+i+1,r.Content,r.Content.toString().ToLeft(10),GetNoticeTypeName(r.NoticeType))}),$("#trNotice").html(t),$("#NoticePages").show()):($("#trNotice").html("<td colspan='3'><h3 style='text-align: center;padding:30px'>暂无相关信息！<\/h3><\/td>"),$("#NoticePages").hide())}})}function GetNoticeTypeName(n){return n==1?"系统公告":n==2?"大赛公告":n==3?"温馨提示":n==4?"资讯快报":void 0}function SaveNotice(){VerificationHelper.checkFrom("addNotice")&&$.ajax({url:"/CompetitionUser/Home/AddNotice",type:"POST",data:{NoticeType:$("#cmbNotice option:selected").val(),Content:$.trim($("#txtNoticeContext").val())},success:function(){dialogHelper.Success({content:"发布公告成功！"});dialogHelper.Close("addNotice");GetNotice()}})}function DetailsNotice(n){$("#addNotice .pop-button").hide();$("#addNotice h3").text("查看公告");$.ajax({url:"/CompetitionUser/Home/GetNoticeModel",type:"POST",async:!1,data:{id:n},success:function(n){$("#cmbNotice").val(n.Data.NoticeType).attr("disabled",!0);$("#txtNoticeContext").val(n.Data.Content).attr("readonly",!0);dialogHelper.Show("addNotice",500)}})}function DeleteNotice(n){dialogHelper.Confirm({content:"确定删除该公告？",success:function(){$.ajax({url:"/CompetitionUser/Home/DeleteNotice",type:"POST",async:!0,data:{id:n},success:function(n){n.IsSuccess?(dialogHelper.Success({content:"删除成功！"}),GetNotice()):dialogHelper.Error({content:n.ErrorCode})}})}})}