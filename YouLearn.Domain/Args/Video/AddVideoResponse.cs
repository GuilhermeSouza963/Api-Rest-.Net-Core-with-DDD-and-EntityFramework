using System;
using System.Collections.Generic;
using System.Text;
using YouLearn.Domain.Entities;

namespace YouLearn.Domain.Args.Video
{
    public class AddVideoResponse
    {
        public Guid idVideo { get; set; }

        public static explicit operator AddVideoResponse(Entities.Video entidade)
        {
            return new AddVideoResponse() { idVideo = entidade.Id };
        }
    }
}
