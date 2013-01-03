using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel; 

namespace SandBox.Pages
{
	/// <summary>
	/// Page1.xaml 的交互逻辑
	/// </summary>
	public partial class TestPage : Page
	{
		public TestPage()
		{
			InitializeComponent();

			ObservableCollection<订单> orderData = new ObservableCollection<订单>();

			orderData.Add(new 订单()
			{
				市场类型 = MarketType.本地市场,
				产品类型 = ProductType.P1,
				单价 = "20"
			});

			orderData.Add(new 订单()
			{
				市场类型 = MarketType.区域市场,
				产品类型 = ProductType.P3,
				单价 = "10"
			});
			
			dataGrid.DataContext = orderData;
		}

	}

	public class 订单
	{
		public MarketType 市场类型 { get; set; }
		public ProductType 产品类型 { get; set; }
		public string 单价 { get; set; }
	}

	public enum MarketType
	{
		本地市场,
		区域市场,
		国内市场,
		亚洲市场,
		国际市场
	}

	public enum ProductType
	{
		P1,
		P2,
		P3,
		P4
	}
}
