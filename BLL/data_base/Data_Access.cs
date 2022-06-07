using BLL.Repository;
using Newtonsoft.Json;
using DAL.Entity.Product;
using DAL.Entity.User;

namespace BLL.data_base
{
	public static class Data_Access
	{
		public static void ReadAllData()
		{
			// ########## User data ##########
			string userPath = @"..\..\DAL\data\User\User_datafile.json";
			string userStr = File.ReadAllText(userPath);
			List<User> userInsertData = JsonConvert.DeserializeObject<List<User>>(userStr);
			UserRepository.User_List = userInsertData;

			// ########## Graphics card data ##########
			string graphPath = @"..\..\DAL\data\Product\GrapchicsCard_datafile.json";
			string graphStr = File.ReadAllText(graphPath);
			List<GraphicsCard> graphInsertData = JsonConvert.DeserializeObject<List<GraphicsCard>>(graphStr);
			ProductsRepository.GraphicsCard_List = graphInsertData;

			// ########## Processor data ##########
			string procPath = @"..\..\DAL\data\Product\Processor_datafile.json";
			string procStr = File.ReadAllText(procPath);
			List<Processor> ProcInsertedData = JsonConvert.DeserializeObject<List<Processor>>(procStr);
			ProductsRepository.Processor_List = ProcInsertedData;

			// ########## Ram data ##########
			string ramPath = @"..\..\DAL\data\Product\Ram_datafile.json";
			string ramStr = File.ReadAllText(ramPath);
			List<Ram> ramInsertedData = JsonConvert.DeserializeObject<List<Ram>>(ramStr);
			ProductsRepository.Ram_List = ramInsertedData;

			// ########## Motherboard data ##########
			string motherPath = @"..\..\DAL\data\Product\Motherboard_datafile.json";
			string motherStr = File.ReadAllText(motherPath);
			List<Motherboard> motherInsertedData = JsonConvert.DeserializeObject<List<Motherboard>>(motherStr);
			ProductsRepository.Motherboard_List = motherInsertedData;
		}

		public static void WriteAllData()
		{
			// ########## User data ##########
			string userStr = JsonConvert.SerializeObject(UserRepository.User_List);
			string userPath = @"..\..\DAL\data\User\User_datafile.json";
			File.WriteAllText(userPath, userStr);

			// ########## Graphics card data ##########
			string graphStr = JsonConvert.SerializeObject(ProductsRepository.GraphicsCard_List);
			string graphPath = @"..\..\DAL\data\Product\GrapchicsCard_datafile.json";
			File.WriteAllText(graphPath, graphStr);

			// ########## Processor data ##########
			string procStr = JsonConvert.SerializeObject(ProductsRepository.Processor_List);
			string procPath = @"..\..\DAL\data\Product\Processor_datafile.json";
			File.WriteAllText(procPath, procStr);

			// ########## Raam data ##########
			string ramStr = JsonConvert.SerializeObject(ProductsRepository.Ram_List);
			string ramPath = @"..\..\DAL\data\Product\Ram_datafile.json";
			File.WriteAllText(ramPath, ramStr);

			// ########## MotherBoard data ##########
			string motherStr = JsonConvert.SerializeObject(ProductsRepository.Motherboard_List);
			string motherPath = @"..\..\DAL\data\Product\Motherboard_datafile.json";
			File.WriteAllText(motherPath, motherStr);
		}
	}
}
