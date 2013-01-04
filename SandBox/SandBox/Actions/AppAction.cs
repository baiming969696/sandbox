using System;
using System.Collections;

namespace SandBox.Actions
{	
	public class AppAction
	{
		public String name;
		public Role role;
		public Mode mode;

		public Year year;
		public Season season;
		public Page page;
		public ArrayList siblingPages;

		public event UpdateDelegate Update_Event;
		public event WarningDelegate Warning_Event;

		#region TypeDefinition

		public enum Role
		{
			/// <summary>
			/// 玩家
			/// </summary>
			Player = 0,

			/// <summary>
			/// 管理员
			/// </summary>
			Admin = 1
		}

		public enum Mode
		{
			/// <summary>
			/// 本地
			/// </summary>
			Local = 0,

			/// <summary>
			/// 局域网
			/// </summary>
			LAN = 1
		}

		/// <summary>
		/// 对应SandBox.Pages的枚举类型
		/// </summary>
		public enum Page
		{
			/// <summary>
			/// 空白页面
			/// </summary>
			BlankPage = 0,

			/// <summary>
			/// 建立服务器
			/// </summary>
			LANPage_Admin,

			/// <summary>
			/// 连接服务器
			/// </summary>
			LANPage_Player,

			/// <summary>
			/// 玩家提交状态
			/// </summary>
			PlayerStatePage,

			/// <summary>
			/// 新年度规划会议
			/// </summary>
			NewYearPlanningPage = 100,

			/// <summary>
			/// 广告投放
			/// </summary>
			PutAdvertisementPage,

			/// <summary>
			/// 登记订单
			/// </summary>
			EnterOrderPage,

			/// <summary>
			/// 支付税款
			/// </summary>
			PayDutyPage,

			BeginningSeason_END,

			/// <summary>
			/// 短期贷款管理
			/// </summary>
			ManageShortTermLoanPage = 200,

			/// <summary>
			/// 原材料入库
			/// </summary>
			TransportMaterialPage,

			/// <summary>
			/// 原材料下单
			/// </summary>
			OrderMaterialPage,

			/// <summary>
			/// 更新生产
			/// </summary>
			FinishProcessPage,

			/// <summary>
			/// 开始下批生产
			/// </summary>
			StartProcessPage,

			/// <summary>
			/// 更新应收款
			/// </summary>
			GatheringPage,

			/// <summary>
			/// 按订单交货
			/// </summary>      
			DeliveryProductPage,

			/// <summary>
			/// 新产品研发投资
			/// </summary>
			DevelopProductPage,

			/// <summary>
			/// 支付行政管理费用
			/// </summary>
			PayExpensePage,

			Season_END,

			/// <summary>
			/// 开拓新市场
			/// </summary>
			ExploitMarketPage = 300,

			/// <summary>
			/// 长期贷款管理
			/// </summary>
            ManageLongTermLoanPage,

			/// <summary>
			/// 设备维护费支付
			/// </summary>
			PayUpKeepPage,

			/// <summary>
			/// 计提折旧
			/// </summary>
			DepreciationPage,

			/// <summary>
			/// 领取新市场准入资格
			/// </summary>
			MarketAccessPage,

			EndingSeason_END
		}

		public enum Year
		{
			BeginningYear = 0,
			FirstYear,
			SecondYear,
			ThirdYear,
			FourthYear,
			FifthYear,
			SixthYear,
			SeventhYear,
			EightthYear,
			NinthYear,
			TenthYear
		}

		public enum Season
		{
			BeginningSeason = 0,
			FirstSeason,
			SecondSeason,
			ThirdSeason,
			FourthSeason,
			EndingSeason,
			Season_END
		}

		public delegate void UpdateDelegate();
		public delegate void WarningDelegate(string str);

		#endregion
		
		public void Initialize()
		{
			if (role == Role.Player)
			{
				if (mode == Mode.Local)
				{
					year = Year.FirstYear;
					season = Season.BeginningSeason;
					page = Page.NewYearPlanningPage;
				}
				else
				{
					year = Year.BeginningYear;
					season = Season.Season_END;
					page = Page.LANPage_Player;
				}
			}
			else 
			{
				if (mode == Mode.Local)
				{
					year = Year.BeginningYear;
					season = Season.Season_END;
					page = Page.BlankPage;
				}
				else
				{
					year = Year.BeginningYear;
					season = Season.Season_END;
					page = Page.LANPage_Admin;
				}
			}

			GetSiblingPages();
		}

		public void Update()
		{
			if (role == Role.Player)
			{
				if (mode == Mode.Local)
				{
					page += 1;
					switch (page)
					{ 
						case Page.BeginningSeason_END:
							season += 1;
							page = Page.ManageShortTermLoanPage;
							GetSiblingPages();
							break;
						case Page.Season_END:
							season += 1;
							page = (season < Season.EndingSeason) ? Page.ManageShortTermLoanPage : Page.ExploitMarketPage;
							GetSiblingPages();
							break;
						case Page.EndingSeason_END:
							year += 1;
							season = Season.BeginningSeason;
							page = Page.NewYearPlanningPage;
							GetSiblingPages();
							break;
					}
				}
				//else
				//{
				//	year = Year.BeginningYear;
				//	season = Season.Season_END;
				//	page = Page.LANPage_Player;
				//}
			}
			else
			{
				//if (mode == Mode.Local)
				//{
				//	year = Year.BeginningYear;
				//	season = Season.Season_END;
				//	page = Page.BlankPage;
				//}
				//else
				//{
				//	year = Year.BeginningYear;
				//	season = Season.Season_END;
				//	page = Page.LANPage_Admin;
				//}
			}

			// Call MainWindow to complete
			Update_Event();
		}

		private void GetSiblingPages()
		{
			siblingPages = new ArrayList();

			switch (season)
			{
				case Season.BeginningSeason:
					for (Page p = Page.NewYearPlanningPage; p < Page.BeginningSeason_END; p++)
					{
						siblingPages.Add(p);
					}
					break;
				case Season.FirstSeason:
				case Season.SecondSeason:
				case Season.ThirdSeason:
				case Season.FourthSeason:
					for (Page p = Page.ManageShortTermLoanPage; p < Page.Season_END; p++)
					{
						siblingPages.Add(p);
					}
					break;
				case Season.EndingSeason:
					for (Page p = Page.ExploitMarketPage; p < Page.EndingSeason_END; p++)
					{
						siblingPages.Add(p);
					}
					break;
			}
		}

		public double GetPhase()
		{
			if (siblingPages.Count <= 0)
				return 0;

			double count = siblingPages.Count;
			switch (season)
			{
				case Season.BeginningSeason:
					return (page - Page.NewYearPlanningPage) / count;
				case Season.FirstSeason:
				case Season.SecondSeason:
				case Season.ThirdSeason:
				case Season.FourthSeason:
					return (page - Page.ManageShortTermLoanPage) / count;
				case Season.EndingSeason:
					return (page - Page.ExploitMarketPage) / count;
			}
			return 0;
		}

		public void WarningBox(string str)
		{
			Warning_Event(str);
		}


	}
}
