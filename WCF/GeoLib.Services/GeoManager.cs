using System;
using System.Collections.Generic;
using System.Linq;
using GeoLib.Contracts;
using GeoLib.Data;

namespace GeoLib.Services
{
    public class GeoManager : IGeoService
    {
	    public GeoManager()
	    {
		    
	    }
	    public GeoManager(IZipCodeRepository zipCodeRepository, IStateRepository stateRepository)
	    {
		    _zipCodeRepository = zipCodeRepository;
		    _stateRepository = stateRepository;
	    }

		public GeoManager(IZipCodeRepository zipCodeRepository)
		{
			_zipCodeRepository = zipCodeRepository;
		}

		public GeoManager(IStateRepository stateRepository)
		{
			_stateRepository = stateRepository;
		}

	    private IZipCodeRepository _zipCodeRepository = null;
	    private IStateRepository _stateRepository = null;

	    public ZipCodeData GetZipInfo(string zip)
	    {
		    ZipCodeData zipCodeData = null;

		    var zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();
		    ZipCode zipCodeEntity = zipCodeRepository.GetByZip(zip);
		    if (zipCodeEntity != null)
		    {
			    zipCodeData = new ZipCodeData()
			    {
				    City = zipCodeEntity.City,
					State = zipCodeEntity.State.Abbreviation,
					ZipCode = zipCodeEntity.Zip
			    };
		    }
		    return zipCodeData;
	    }

	    public IEnumerable<string> GetStates(bool primaryOnly)
	    {
		    var stateData = new List<string>();

			IStateRepository stateRepository =_stateRepository ?? new StateRepository();

		    IEnumerable<State> states = stateRepository.Get(primaryOnly);

		    if (states != null)
		    {
			    stateData.AddRange(states.Select(state => state.Abbreviation));
		    }

		    return stateData;
	    }

	    public IEnumerable<ZipCodeData> GetZips(string state)
	    {
			var zipCodeData = new List<ZipCodeData>();

			var zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();

		    var zips = zipCodeRepository.GetByState(state);
		    if (zips != null)
		    {
			    zipCodeData.AddRange(zips.Select(zipCode => new ZipCodeData()
			    {
				    City = zipCode.City, State = zipCode.State.Abbreviation, ZipCode = zipCode.Zip
			    }));
		    }

		    return zipCodeData;
	    }

	    public IEnumerable<ZipCodeData> GetZips(string zip, int range)
	    {
			var zipCodeData = new List<ZipCodeData>();

			var zipCodeRepository = _zipCodeRepository ?? new ZipCodeRepository();

		    var zipEntity = zipCodeRepository.GetByZip(zip);

			var zips = zipCodeRepository.GetZipsForRange(zipEntity, range);
			if (zips != null)
			{
				zipCodeData.AddRange(zips.Select(zipCode => new ZipCodeData()
				{
					City = zipCode.City,
					State = zipCode.State.Abbreviation,
					ZipCode = zipCode.Zip
				}));
			}

			return zipCodeData;
	    }
    }
}
