using DAL.Entity.Product;
using DAL.Entity;


namespace BLL.Repository
{
    public class ProductsRepository
    {

        public static List<GraphicsCard> GraphicsCard_List { get; set; } = new List<GraphicsCard>();
        public static List<Processor> Processor_List { get; set; } = new List<Processor>();
        public static List<Ram> Ram_List { get; set; } = new List<Ram>();
        public static List<Motherboard> Motherboard_List { get; set; } = new List<Motherboard>();

        #region GraphicsCard
        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newGraphicsCard"></param>
        /// <returns>input</returns>
        public static GraphicsCard CreateGraphicsCard(string name, double price, int off, int rating, string image, User whoHasMade)
        {
            if (GraphicsCard_List.Where(x => x.Name.ToLower() == name.ToLower()).Any())
            {
                throw new Exception("Graphics card already exists");
            }
            var newGraphicsCard = new GraphicsCard(name, price, off, rating, image, whoHasMade);
            GraphicsCard_List.Add(newGraphicsCard);
            return newGraphicsCard;
        }

        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newGraphicsCard"></param>
        /// <returns>input</returns>
        public static GraphicsCard CreateGraphicsCard(string name, double price, int off, int rating, User whoHasMade)
        {
            if (GraphicsCard_List.Where(x => x.Name.ToLower() == name.ToLower()).Any())
            {
                throw new Exception("Graphics card already exists");
            }
            var newGraphicsCard = new GraphicsCard(name, price, off, rating, "../Images/GPU.jpg", whoHasMade);
            GraphicsCard_List.Add(newGraphicsCard);
            return newGraphicsCard;
        }

        /// <summary>
        /// returns the GraphicsCard by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GraphicsCard</returns>
        public static GraphicsCard GetGraphicsCardById(int id)
        {
            return GraphicsCard_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the GraphicsCards with given 
        /// </summary>
        /// <param name="inputGraphicsCard"></param>
        /// <returns>List<GraphicsCard></returns>
        public static List<GraphicsCard> SearchGraphicsCard(GraphicsCard inputGraphicsCard)
        {
            return GraphicsCard_List.Where(x => x == inputGraphicsCard).ToList();
        }

        /// <summary>
        /// updates the GraphicsCard
        /// </summary>
        /// <param name="inputGraphicsCard"></param>
        /// <returns>input</returns>
        public static GraphicsCard UpdateGraphicsCard(GraphicsCard inputGraphicsCard)
        {
            if (!GraphicsCard_List.Exists(x => x.Id == inputGraphicsCard.Id))
            {
                throw new Exception("GraphicsCard doesn't exist");
            }
            int GraphicsCardIndex = GraphicsCard_List.FindIndex(x => x.Id == inputGraphicsCard.Id);
            GraphicsCard_List[GraphicsCardIndex] = inputGraphicsCard;
            return inputGraphicsCard;
        }

        /// <summary>
        /// deletes the GraphicsCard from GraphicsCard_List
        /// </summary>
        /// <param name="id"></param>
        public static bool DeleteGraphicsCard(int id)
        {
            if (!GraphicsCard_List.Exists(x => x.Id == id))
            {
                return false;
            }

            var graphicsCard = GraphicsCard_List.SingleOrDefault(x => x.Id == id);
            GraphicsCard_List.Remove(graphicsCard);
            return true;
        }
        #endregion

        #region Processor
        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newProcessor"></param>
        /// <returns>input</returns>
        public static Processor CreateProcessor(string name, double price, int off, int rating, string image, User whoHasMade)
        {
            if (Processor_List.Where(x => x.Name.ToLower() == name.ToLower()).Any())
            {
                throw new Exception("Processor already exists");
            }
            var newProcessor = new Processor(name, price, off, rating, image, whoHasMade);
            Processor_List.Add(newProcessor);
            return newProcessor;
        }

        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newProcessor"></param>
        /// <returns>input</returns>
        public static Processor CreateProcessor(string name, double price, int off, int rating, User whoHasMade)
        {
            if (Processor_List.Where(x => x.Name.ToLower() == name.ToLower()).Any())
            {
                throw new Exception("Processor already exists");
            }
            var newProcessor = new Processor(name, price, off, rating, "../Images/CPU.jpg", whoHasMade);
            Processor_List.Add(newProcessor);
            return newProcessor;
        }

        /// <summary>
        /// returns the Processor by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Processor</returns>
        public static Processor GetProcessorById(int id)
        {
            return Processor_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the Processors with given 
        /// </summary>
        /// <param name="inputProcessor"></param>
        /// <returns>List<Processor></returns>
        public static List<Processor> SearchProcessor(Processor inputProcessor)
        {
            return Processor_List.Where(x => x == inputProcessor).ToList();
        }

        /// <summary>
        /// updates the Processor
        /// </summary>
        /// <param name="inputProcessor"></param>
        /// <returns>input</returns>
        public static Processor UpdateProcessor(Processor inputProcessor)
        {
            if (!Processor_List.Exists(x => x.Id == inputProcessor.Id))
            {
                throw new Exception("Processor doesn't exist");
            }
            int ProcessorIndex = Processor_List.FindIndex(x => x.Id == inputProcessor.Id);
            Processor_List[ProcessorIndex] = inputProcessor;
            return inputProcessor;
        }

        /// <summary>
        /// deletes the Processor from Processor_List
        /// </summary>
        /// <param name="id"></param>
        public static bool DeleteProcessor(int id)
        {
            if (!Processor_List.Exists(x => x.Id == id))
            {
                return false;
            }

            var processor = Processor_List.SingleOrDefault(x => x.Id == id);
            Processor_List.Remove(processor);
            return true;

        }
        #endregion

        #region RAM
        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newRam"></param>
        /// <returns>input</returns>
        public static Ram CreateRam(string name, double price, int off, int rating, string image, User whoHasMade)
        {
            if (Ram_List.Where(x => x.Name.ToLower() == name.ToLower()).Any())
            {
                throw new Exception("RAM already exists");
            }
            var newRam = new Ram(name, price, off, rating, image, whoHasMade);
            Ram_List.Add(newRam);
            return newRam;
        }

        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newRam"></param>
        /// <returns>input</returns>
        public static Ram CreateRam(string name, double price, int off, int rating, User whoHasMade)
        {
            if (Ram_List.Where(x => x.Name.ToLower() == name.ToLower()).Any())
            {
                throw new Exception("RAM already exists");
            }
            var newRam = new Ram(name, price, off, rating, "../Images/RAM.jpg", whoHasMade);
            Ram_List.Add(newRam);
            return newRam;
        }

        /// <summary>
        /// returns the Ram by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ram</returns>
        public static Ram GetRamById(int id)
        {
            return Ram_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the Rams with given 
        /// </summary>
        /// <param name="inputRam"></param>
        /// <returns>List<Ram></returns>
        public static List<Ram> SearchRam(Ram inputRam)
        {
            return Ram_List.Where(x => x == inputRam).ToList();
        }

        /// <summary>
        /// updates the Ram
        /// </summary>
        /// <param name="inputRam"></param>
        /// <returns>input</returns>
        public static Ram UpdateRam(Ram inputRam)
        {
            if (!Ram_List.Exists(x => x.Id == inputRam.Id))
            {
                throw new Exception("Ram doesn't exist");
            }
            int RamIndex = Ram_List.FindIndex(x => x.Id == inputRam.Id);
            Ram_List[RamIndex] = inputRam;
            return inputRam;
        }

        /// <summary>
        /// deletes the Ram from Ram_List
        /// </summary>
        /// <param name="id"></param>
        public static bool DeleteRam(int id)
        {
            if (!Ram_List.Exists(x => x.Id == id))
            {
                return false;
            }

            var ram = Ram_List.SingleOrDefault(x => x.Id == id);
            Ram_List.Remove(ram);
            return true;
        }
        #endregion

        #region Motherboard
        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newGraphicsCard"></param>
        /// <returns>input</returns>
        public static Motherboard CreateMotherboard(string name, double price, int off, int rating, string image, User whoHasMade)
        {
            if (Motherboard_List.Where(x => x.Name.ToLower() == name.ToLower()).Any())
            {
                throw new Exception("Motherboard already exists");
            }
            var newMotherboard = new Motherboard(name, price, off, rating, image, whoHasMade);
            Motherboard_List.Add(newMotherboard);
            return newMotherboard;
        }

        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newGraphicsCard"></param>
        /// <returns>input</returns>
        public static Motherboard CreateMotherboard(string name, double price, int off, int rating, User whoHasMade)
        {
            if (Motherboard_List.Where(x => x.Name.ToLower() == name.ToLower()).Any())
            {
                throw new Exception("Motherboard already exists");
            }
            var newMotherboard = new Motherboard(name, price, off, rating, "../Images/Motherboard.jpg", whoHasMade);
            Motherboard_List.Add(newMotherboard);
            return newMotherboard;
        }

        /// <summary>
        /// returns the Motherboard by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Motherboard</returns>
        public static Motherboard GetMotherboardById(int id)
        {
            return Motherboard_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the Motherboards with given 
        /// </summary>
        /// <param name="inputMotherboard"></param>
        /// <returns>List<Motherboard></returns>
        public static List<Motherboard> SearchMotherboard(Motherboard inputMotherboard)
        {
            return Motherboard_List.Where(x => x == inputMotherboard).ToList();
        }

        /// <summary>
        /// updates the Motherboard
        /// </summary>
        /// <param name="inputMotherboard"></param>
        /// <returns>input</returns>
        public static Motherboard UpdateMotherboard(Motherboard inputMotherboard)
        {
            if (!Motherboard_List.Exists(x => x.Id == inputMotherboard.Id))
            {
                throw new Exception("Motherboard doesn't exist");
            }
            int MotherboardIndex = Motherboard_List.FindIndex(x => x.Id == inputMotherboard.Id);
            Motherboard_List[MotherboardIndex] = inputMotherboard;
            return inputMotherboard;
        }

        /// <summary>
        /// deletes the Motherboard from Motherboard_List
        /// </summary>
        /// <param name="id"></param>
        public static bool DeleteMotherboard(int id)
        {
            if (!Motherboard_List.Exists(x => x.Id == id))
            {
                return false;
            }

            var motherboard = Motherboard_List.SingleOrDefault(x => x.Id == id);
            Motherboard_List.Remove(motherboard);
            return true;

        }
        #endregion

    }
}
