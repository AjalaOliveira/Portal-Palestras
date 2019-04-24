using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Palestras.Domain.Core.Events;

namespace Palestras.Application.EventSourcedNormalizers
{
    public class PalestranteHistory
    {
        public static IList<PalestranteHistoryData> HistoryData { get; set; }

        public static IList<PalestranteHistoryData> ToJavaScriptPalestranteHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<PalestranteHistoryData>();
            PalestranteHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<PalestranteHistoryData>();
            var last = new PalestranteHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new PalestranteHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    Nome = string.IsNullOrWhiteSpace(change.Nome) || change.Nome == last.Nome
                        ? ""
                        : change.Nome,
                    MiniBio = string.IsNullOrWhiteSpace(change.MiniBio) || change.MiniBio == last.MiniBio
                        ? ""
                        : change.MiniBio,
                    Url = string.IsNullOrWhiteSpace(change.Url) || change.Url == last.Url
                        ? ""
                        : change.Url,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }

            return list;
        }

        private static void PalestranteHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new PalestranteHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "PalestranteRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.MiniBio = values["MiniBio"];
                        slot.Url = values["Url"];
                        slot.Nome = values["Nome"];
                        slot.Action = "Cadastrado";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "PalestranteUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.MiniBio = values["MiniBio"];
                        slot.Url = values["Url"];
                        slot.Nome = values["Nome"];
                        slot.Action = "Atualizado";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "PalestranteRemovedEvent":
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