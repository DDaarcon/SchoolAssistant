using SchoolAssistant.DAL.Models.Rooms;
using SchoolAssistant.DAL.Repositories;
using SchoolAssistant.Infrastructure.Models.DataManagement.Rooms;
using SchoolAssistant.Infrastructure.Models.Shared.Json;

namespace SchoolAssistant.Logic.DataManagement.Rooms
{
    public interface IModifyRoomFromJsonService
    {
        Task<ResponseJson> CreateOrUpdateAsync(RoomDetailsJson model);
    }


    // TODO: Extract common part of every 'ModifyFromJsonService' to base service
    [Injectable]
    public class ModifyRoomFromJsonService : IModifyRoomFromJsonService
    {
        private readonly IRepository<Room> _repo;

        private Room _entity = null!;
        private RoomDetailsJson _model = null!;
        private ResponseJson _response = null!;

        public ModifyRoomFromJsonService(
            IRepository<Room> roomRepo)
        {
            _repo = roomRepo;
        }

        public async Task<ResponseJson> CreateOrUpdateAsync(RoomDetailsJson model)
        {
            _model = model;
            _response = new ResponseJson();

            if (!await ValidateAsync())
                return _response;

            if (_model.id.HasValue)
                await UpdateAsync();
            else
                await CreateAsync();

            return _response;
        }

        private async Task<bool> ValidateAsync()
        {
            if (_model is null)
            {
                _response.message = "Błąd! Brakuje modelu";
                return false;
            }

            if (_model.id.HasValue && !await _repo.ExistsAsync(_model.id!.Value))
            {
                _response.message = "Błąd! Modyfikowane pomieszczenie nie istnieje";
                return false;
            }

            else if (await _repo.ExistsAsync(x => x.Name == _model.name && x.Number == _model.number))
            {
                _response.message = "Istnieje już pomieszczenie o tej samej nazwie i numerze";
                return false;
            }

            if (String.IsNullOrEmpty(_model.name))
            {
                _response.message = "Brakuje nazwy pomieszczenia";
                return false;
            }

            return true;
        }

        private async Task UpdateAsync()
        {
            _entity = (await _repo.GetByIdAsync(_model.id!.Value))!;

            AssignValuesFromModel();

            await _repo.SaveAsync();
        }

        private async Task CreateAsync()
        {
            _entity = new Room();

            AssignValuesFromModel();

            _repo.Add(_entity);
            await _repo.SaveAsync();
        }

        private void AssignValuesFromModel()
        {
            _entity.Name = _model.name;
            _entity.Number = _model.number;
            _entity.Floor = _model.floor;
        }
    }
}
