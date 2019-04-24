namespace Palestras.Application.EventSourcedNormalizers
{
    public class PalestraHistoryData
    {
        public string Action { get; set; }
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Data { get; set; }
        public string PalestranteId { get; set; }
        public string When { get; set; }
        public string Who { get; set; }
    }
}