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
                    Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
                        ? ""
                        : change.Name,
                    Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
                        ? ""
                        : change.Email,
                    BirthDate = string.IsNullOrWhiteSpace(change.BirthDate) || change.BirthDate == last.BirthDate
                        ? ""
                        : change.BirthDate.Substring(0, 10),
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
                        slot.BirthDate = values["BirthDate"];
                        slot.Email = values["Email"];
                        slot.Name = values["Name"];
                        slot.Action = "Cadastrado";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "PalestranteUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.BirthDate = values["BirthDate"];
                        slot.Email = values["Email"];
                        slot.Name = values["Name"];
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