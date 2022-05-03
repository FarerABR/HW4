using DAL.Entity.Product;
using System;

namespace BLL.Repository
{
    public class ProductRepository
    {

        public List<GraphicsCard> GraphicsCard_List { get; set; }
        public List<Processor> Processor_List { get; set; }
        public List<Ram> Ram_List { get; set; }
        public List<Motherboard> Motherboard_List { get; set; }

        #region GraphicsCard
        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newGraphicsCard"></param>
        /// <returns>input</returns>
        public GraphicsCard CreateGraphicsCard(GraphicsCard newGraphicsCard)
        {
            if (GraphicsCard_List.Where(x => x == newGraphicsCard).Any())
            {
                throw new Exception("GraphicsCard already exist");
            }
            GraphicsCard_List.Add(newGraphicsCard);
            return newGraphicsCard;
        }

        /// <summary>
        /// returns the GraphicsCard by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>GraphicsCard</returns>
        public GraphicsCard GetGraphicsCardById(int id)
        {
            return GraphicsCard_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the GraphicsCards with given 
        /// </summary>
        /// <param name="inputGraphicsCard"></param>
        /// <returns>List<GraphicsCard></returns>
        public List<GraphicsCard> SearchGraphicsCard(GraphicsCard inputGraphicsCard)
        {
            return GraphicsCard_List.Where(x => x == inputGraphicsCard).ToList();
        }

        /// <summary>
        /// updates the GraphicsCard
        /// </summary>
        /// <param name="inputGraphicsCard"></param>
        /// <returns>input</returns>
        public GraphicsCard UpdateGraphicsCard(GraphicsCard inputGraphicsCard)
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
        public void DeleteGraphicsCard(int id)
        {
            if (!GraphicsCard_List.Exists(x => x.Id == id))
            {
                throw new Exception("GraphicsCard doesn't exist");
            }

            var graphicsCard = GraphicsCard_List.SingleOrDefault(x => x.Id == id);
            if (graphicsCard.IsDeleted == true)
            {
                throw new Exception("GraphicsCard is already deleted");
            }

            graphicsCard.IsDeleted = true;

        }
        #endregion
        #region Processor
        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newProcessor"></param>
        /// <returns>input</returns>
        public Processor CreateProcessor(Processor newProcessor)
        {
            if (Processor_List.Where(x => x == newProcessor).Any())
            {
                throw new Exception("Processor already exist");
            }
            Processor_List.Add(newProcessor);
            return newProcessor;
        }

        /// <summary>
        /// returns the Processor by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Processor</returns>
        public Processor GetProcessorById(int id)
        {
            return Processor_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the Processors with given 
        /// </summary>
        /// <param name="inputProcessor"></param>
        /// <returns>List<Processor></returns>
        public List<Processor> SearchProcessor(Processor inputProcessor)
        {
            return Processor_List.Where(x => x == inputProcessor).ToList();
        }

        /// <summary>
        /// updates the Processor
        /// </summary>
        /// <param name="inputProcessor"></param>
        /// <returns>input</returns>
        public Processor UpdateProcessor(Processor inputProcessor)
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
        public void DeleteProcessor(int id)
        {
            if (!Processor_List.Exists(x => x.Id == id))
            {
                throw new Exception("Processor doesn't exist");
            }

            var processor = Processor_List.SingleOrDefault(x => x.Id == id);
            if (processor.IsDeleted == true)
            {
                throw new Exception("Processor is already deleted");
            }

            processor.IsDeleted = true;

        }
        #endregion
        #region RAM
        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newRam"></param>
        /// <returns>input</returns>
        public Ram CreateRam(Ram newRam)
        {
            if (Ram_List.Where(x => x == newRam).Any())
            {
                throw new Exception("Ram already exist");
            }
            Ram_List.Add(newRam);
            return newRam;
        }

        /// <summary>
        /// returns the Ram by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Ram</returns>
        public Ram GetRamById(int id)
        {
            return Ram_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the Rams with given 
        /// </summary>
        /// <param name="inputRam"></param>
        /// <returns>List<Ram></returns>
        public List<Ram> SearchRam(Ram inputRam)
        {
            return Ram_List.Where(x => x == inputRam).ToList();
        }

        /// <summary>
        /// updates the Ram
        /// </summary>
        /// <param name="inputRam"></param>
        /// <returns>input</returns>
        public Ram UpdateRam(Ram inputRam)
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
        public void DeleteRam(int id)
        {
            if (!Ram_List.Exists(x => x.Id == id))
            {
                throw new Exception("Ram doesn't exist");
            }

            var ram = Ram_List.SingleOrDefault(x => x.Id == id);
            if (ram.IsDeleted == true)
            {
                throw new Exception("Ram is already deleted");
            }

            ram.IsDeleted = true;

        }
        #endregion
        #region Motherboard
        /// <summary>
        /// creates new GraphicsCard
        /// </summary>
        /// <param name="newGraphicsCard"></param>
        /// <returns>input</returns>
        public Motherboard CreateMotherboard(Motherboard newMotherboard)
        {
            if (Motherboard_List.Where(x => x == newMotherboard).Any())
            {
                throw new Exception("Motherboard already exist");
            }
            Motherboard_List.Add(newMotherboard);
            return newMotherboard;
        }

        /// <summary>
        /// returns the Motherboard by given Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Motherboard</returns>
        public Motherboard GetMotherboardById(int id)
        {
            return Motherboard_List.Where(x => x.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// returns all the Motherboards with given 
        /// </summary>
        /// <param name="inputMotherboard"></param>
        /// <returns>List<Motherboard></returns>
        public List<Motherboard> SearchMotherboard(Motherboard inputMotherboard)
        {
            return Motherboard_List.Where(x => x == inputMotherboard).ToList();
        }

        /// <summary>
        /// updates the Motherboard
        /// </summary>
        /// <param name="inputMotherboard"></param>
        /// <returns>input</returns>
        public Motherboard UpdateMotherboard(Motherboard inputMotherboard)
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
        public void DeleteMotherboard(int id)
        {
            if (!Motherboard_List.Exists(x => x.Id == id))
            {
                throw new Exception("Motherboard doesn't exist");
            }

            var motherboard = Motherboard_List.SingleOrDefault(x => x.Id == id);
            if (motherboard.IsDeleted == true)
            {
                throw new Exception("Motherboard is already deleted");
            }

            motherboard.IsDeleted = true;

        }
        #endregion
    }
}
