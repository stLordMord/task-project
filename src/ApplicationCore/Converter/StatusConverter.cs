using ApplicationCore;
using Common;
using Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Converter
{
    public class StatusConverter : IConverter<StatusDTO, StatusBLO>
    {
        private readonly ILogger<StatusConverter> logger;
        public StatusConverter(ILogger<StatusConverter> logger)
        {
            this.logger = logger;
        }
        public IList<StatusBLO> Convert(IList<StatusDTO> listDTO)
        {
            if (listDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(listDTO));
            }
            List<StatusBLO> statuses = new List<StatusBLO>();
            foreach (StatusDTO statusDTO in listDTO)
            {
                StatusBLO status = Convert(statusDTO);
                statuses.Add(status);
            }
            return statuses;
        }

        public StatusBLO Convert(StatusDTO statusDTO)
        {
            if (statusDTO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(statusDTO));
            }
            StatusBLO statusBLO = new StatusBLO()
            {
                Id = statusDTO.Id,
                Name = statusDTO.Name
            };
            return statusBLO;
        }

        public StatusDTO Convert(StatusBLO statusBLO)
        {
            if (statusBLO == null)
            {
                logger.LogError("Значение не может быть null");
                throw new ArgumentNullException(nameof(statusBLO));
            }
            StatusDTO statusDTO = new StatusDTO()
            {
                Id = statusBLO.Id,
                Name = statusBLO.Name
            };
            return statusDTO;
        }
    }
}
