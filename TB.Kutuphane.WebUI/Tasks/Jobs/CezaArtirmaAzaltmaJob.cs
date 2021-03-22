using Quartz;
using System;
using TB.Kutuphane.Data.UnitOfWork;
using TB.Kutuphane.Entity;

namespace TB.Kutuphane.WebUI.Tasks.Jobs
{
    public class CezaArtirmaAzaltmaJob : IJob
    {
        UnitOfWork _unitOfWork;
        public CezaArtirmaAzaltmaJob()
        {
            _unitOfWork = new UnitOfWork();
        }

        public void Execute(IJobExecutionContext context)
        {
            try
            {
                CezaArtir();
                CezaAzalt();
                _unitOfWork.SaveChanges();
            }
            catch { }
        }

        private void CezaArtir()
        {
            var getOduncKitap = _unitOfWork.GetRepository<OduncKitap>().GetAll(x => x.GetirdigiTarih == null && DateTime.Now > x.GetirecegiTarih);
            foreach (var item in getOduncKitap)
            {
                item.Uyeler.Ceza += 1;
                _unitOfWork.GetRepository<Uye>().Update(item.Uyeler);
            }
        }

        private void CezaAzalt()
        {
            var getOduncKitap = _unitOfWork.GetRepository<OduncKitap>().GetAll(x => x.GetirdigiTarih != null && x.Uyeler.Ceza > 0);
            foreach (var item in getOduncKitap)
            {
                item.Uyeler.Ceza -= 1;
                _unitOfWork.GetRepository<Uye>().Update(item.Uyeler);
            }
        }
    }
}