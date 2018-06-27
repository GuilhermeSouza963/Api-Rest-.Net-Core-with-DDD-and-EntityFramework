using System;
using System.Collections.Generic;
using System.Text;

namespace YouLearn.Domain.Args.Video
{
    public class AddVideoRequest
    {
        public Guid IdCanal { get; set; }
        public Guid IdPlayList { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Tags { get; set; }
        public int? OrdemPlayList { get; set; }
        public string IdVideoYoutube { get; set; }

    }
}
