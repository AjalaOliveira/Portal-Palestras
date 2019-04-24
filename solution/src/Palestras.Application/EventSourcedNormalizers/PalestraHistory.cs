using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Palestras.Domain.Core.Events;

namespace Palestras.Application.EventSourcedNormalizers
{
    public class PalestraHistory
    {
        public static IList<PalestraHistoryData> HistoryData { get; set; }

        public static IList<PalestraHistoryData> ToJavaScriptPalestraHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<PalestraHistoryData>();
            PalestraHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<PalestraHistoryData>();
            var last = new PalestraHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new PalestraHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Titulo = string.IsNullOrWhiteSpace(change.Titulo) || change.Titulo == last.Titulo
                        ? ""
                        : change.Titulo,
                    Descricao = string.IsNullOrWhiteSpace(change.Descricao) || change.Descricao == last.Descricao
                        ? ""
                        : change.Descricao,
                    Data = string.IsNullOrWhiteSpace(change.Data) || change.Data == last.Data
                        ? ""
                        : change.Data.Substring(0, 10),
                    PalestranteId = string.IsNullOrWhiteSpace(change.PalestranteId) || change.PalestranteId == last.PalestranteId
                        ? ""
                        : change.Data,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }

            return list;
        }

        private static void PalestraHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new PalestraHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "PalestraRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Data = values["Data"];
                        slot.PalestranteId = values["PalestranteId"];
                        slot.Descricao = values["Descricao"];
                        slot.Titulo = values["Titulo"];
                        slot.Action = "Cadastrado";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "PalestraUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Data = values["Data"];
                        slot.PalestranteId = values["PalestranteId"];
                        slot.Descricao = values["Descricao"];
                        slot.Titulo = values["Titulo"];
                        slot.Action = "Atualizado";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "PalestraRemovedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Action = "Removido";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }

                HistoryData.Add(slot);
            }
        }
    }
}