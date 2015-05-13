'ArtworksAgileworks'
http://wiki.corpautohome.com/pages/viewpage.action?pageId=29463642

http://10.168.0.49/wangyang/yuyue/index.html#p=车智赢首页
'
/* task details */

测试账号：
select * from clubMember where mName='北京亚市联盟二手车' 
select* from Dealers where DealerId=135920
select * from CarOffer where DealerId=135920 order by InsertTime desc


-----------------------
-- 重置数据

begin tran 
update CarOffer set dealerid=82120,OfferStatus=0 where OfferId in (
200,
201,
202,
203,
204);
/*
update CarOffer set InfoId=629678 where OfferId=200
update CarOffer set InfoId=216790 where OfferId=201
update CarOffer set InfoId=282812 where OfferId=202
update CarOffer set InfoId=656868 where OfferId=203
update CarOffer set InfoId=603039 where OfferId=204
*/
-- commit 

添加权限：
begin tran 
insert into ClubMemberRole (memberId,roleId) values (5131097,1)
insert into ClubMemberRole (memberId,roleId) values (5131097,5)
insert into ClubMemberRole (memberId,roleId) values (5131097,10)
insert into ClubMemberRole (memberId,roleId) values (5131097,15)

1.DealerIndex_V2.aspx.cs;DealerIndex_V1.aspx.cs 将“询价管理”修改为：预约询价管理
2.询价预约管理-未处理订单  SellTrail/QuotedPriceList.aspx
	1)点击处理显示如下图，增加意向车源订单类型“询价”，“预约"
	function showlayer1(mobile, layer) 修改

3.新增：主要查询，类型，预约时间和留言内容

declare @dealerid int 
declare @mobile varchar(20)
declare @status int 

set @status=0
set @mobile=13121976420	
set @dealerid=135920

select c.InfoId,c.carname,v.Price,v.Displacement,v.RegisteDate,v.Mileage,v.Gearbox,v.IsPublic,v.IsSelled,v.IsOverTime 
,c.appointmenttime,c.leavemessage,c.OfferType

                    ,(select top 1 sourcePic
					
					
					 from UsedCarPic with(nolock) where 
					c.InfoId=UsedCarPic.infoId and UsedCarPic.picType=10) sourcePic from  CarOffer c with(nolock) 
                    join (

                    select Price,Displacement,RegisteDate,Mileage,Gearbox,InfoId,IsPublic,IsSelled,IsOverTime
					 from QuotePrice with(nolock) where CompanyId=@dealerid and IsOutSite=0
                    union 
                    select Price,Displacement,RegisteDate,Mileage,Gearbox,InfoId,IsPublic,IsSelled,IsOverTime 
					from QuoteCarSelled  with(nolock) where CompanyId=@dealerid and IsOutSite=0 )v
                    on v.InfoId=c.InfoId

                    where dealerid=@dealerid and OfferStatus=@status  and Mobile=@mobile 
                    order by c.InsertTime desc --/WebSite.Dealer.Admin/App_Code/DAL/SellTrail/CarOffer.cs


4.上海车王认证二手车超市（乾瑞名车馆）	登录账号：乾瑞二手车
公司地址：上海市卢湾区中山南一路727号 

登录可测试数据


'
