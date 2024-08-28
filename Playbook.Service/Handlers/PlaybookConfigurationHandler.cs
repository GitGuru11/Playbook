using Microsoft.OpenApi.Validations;
using Playbook.Business.Model;
using Playbook.Bussiness.Model;
using Playbook.Common.Instrumentation;
using Playbook.Data.ClickHouse;
using Playbook.Service.Contracts;
using PlaybookRevision = Playbook.Bussiness.Model.PlaybookRevision;

namespace Playbook.Service.Handlers
{
    public class PlaybookConfigurationHandler
    {
        public async Task<PlaybookResponse> ProcessGetPlaybooksRequestAsync(string? sort_order,
            string? sort_column, string? search_term, int skip = 0, int limit = 10)
        {
            var response = await new PlaybookFactory().FindAllActiveAsync(
                sort_order is null || sort_order.Contains("desc", StringComparison.CurrentCultureIgnoreCase)
                    ? SortOrder.Descending
                    : SortOrder.Ascending,
                sort_column,
                search_term,
                skip,
                limit
            );

            return new PlaybookResponse(playbooks: response, count: response.Count, limit: limit, skip: skip,
                search_term: search_term ?? "");
        }

        public async Task<Bussiness.Model.Playbook> ProcessGetPlaybookRequestAsync(string id)
        {
            var factory = new PlaybookFactory();
            var response = await factory.FindAsync(id);
            if (response is null)
            {
                InstrumentationContext.Current.Error("ProcessGetPlaybookRequestAsync",
                    "Not finding playbook.");
            }

            return response ?? new Bussiness.Model.Playbook();
        }

        public async Task<bool> ProcessCreatePlaybookRequestAsync(PlaybookRequest request)
        {
            var playbookFactory = new PlaybookFactory();
            var definitionFactory = new PlaybookDefinitionFactory();

            var playbook = ConvertEntityToDto(request, true);
            var playbookDefinition = new PlaybookDefinition()
            {
                Id = UuidFactory.GetUuid(),
                //ParentId = playbook.Id,
                IsActive = playbook.Enable,
                VersionNumber = 1,
                ObjectTypeId = playbook.ObjectTypeId,
                IsNew = true
            };

            List<Task> tasks = new()
                { playbookFactory.SaveAsync(playbook), definitionFactory.SaveAsync(playbookDefinition) };
            await Task.WhenAll(tasks);
            return tasks.All(x => x.IsCompletedSuccessfully);
        }

        public async Task<string> ProcessUpdatePlaybookRequestAsync(PlaybookRequest request, string id)
        {
            var playbookFactory = new PlaybookFactory();
            var playbook = await playbookFactory.FindAsync(id);
            if (playbook is not null)
            {
                var definitionFactory = new PlaybookDefinitionFactory();

                playbook.Description = request.Description;
                playbook.Name = request.Name;

                List<Task> tasks = new() { playbookFactory.SaveAsync(playbook) };
                var curNumber = await definitionFactory.LastVersionNumberByParentIdAsync(id) + 1;
                if (!playbook.Enable)
                {
                    var playbookDefinition = new PlaybookDefinition()
                    {
                        Id = UuidFactory.GetUuid(),
                        //ParentId = playbook.Id,
                        IsActive = playbook.Enable,
                        VersionNumber = curNumber,
                        ObjectTypeId = playbook.ObjectTypeId,
                        IsNew = true
                    };

                    tasks.Add(definitionFactory.SaveAsync(playbookDefinition));
                }

                await Task.WhenAll(tasks);

                if (tasks.Any(x => x.IsFaulted))
                {
                    InstrumentationContext.Current.Error("ProcessUpdatePlaybookRequestAsync",
                        "Problem updating enable playbook.");
                }
            }
            else
            {
                InstrumentationContext.Current.Error("ProcessUpdatePlaybookRequestAsync",
                    "Not finding playbook.");
            }

            return id;
        }

        public async Task<string> ProcessEnablePlaybookRequestAsync(string id, bool enable)
        {
            var factory = new PlaybookFactory();
            var playbook = await factory.FindAsync(id);

            if (playbook is not null)
            {
                playbook.Enable = enable;

                if (!await factory.SaveAsync(playbook))
                {
                    InstrumentationContext.Current.Error("ProcessEnablePlaybookRequestAsync",
                        "Problem updating enable playbook.");
                }
            }
            else
            {
                InstrumentationContext.Current.Error("ProcessEnablePlaybookRequestAsync",
                    "Not finding playbook.");
            }

            return id;
        }

        public async Task<string> ProcessDeletePlaybookRequestAsync(string id)
        {
            var factory = new PlaybookFactory();
            var playbook = await factory.FindAsync(id);

            if (playbook is not null)
            {
                playbook.IsDeleted = true;

                if (!await factory.SaveAsync(playbook))
                {
                    InstrumentationContext.Current.Error("ProcessDeletePlaybookRequestAsync",
                        "Problem updating enable playbook.");
                }
            }
            else
            {
                InstrumentationContext.Current.Error("ProcessDeletePlaybookRequestAsync",
                    "Not finding playbook.");
            }

            return id;
        }

        public async Task<List<PlaybookRevision>> ProcessGetPlaybookRevisionsRequestAsync(string? playbookId = null)
        {
            return await new PlaybookRevisionFactory().FindRevisionsPlaybooksByIdAsync(playbookId);
        }

        public async Task<List<PlaybookDefinition>> ProcessGetPlaybookVersionsRequestAsync(string id)
        {
            return await new PlaybookDefinitionFactory().FindDefinitionsByParentIdAsync(id);
        }

        private Bussiness.Model.Playbook ConvertEntityToDto(PlaybookRequest item, bool isNew = false) =>
            new()
            {
                Id = UuidFactory.GetUuid(),
                Name = item.Name,
                Description = item.Description,
                ObjectTypeId = item.ObjectTypeId,
                Enable = false,
                LastRun = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsNew = isNew
            };
    }
}