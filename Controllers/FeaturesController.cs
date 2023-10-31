using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using Viettel_Solution.DTO;
using Viettel_Solution.Models;
using Viettel_Solution.Storage;

namespace ViettelSolutions.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeaturesController : ControllerBase
    {
        private readonly ViettelSolutionDbConext _context;
        private readonly IStorageService _storageService;
        private readonly IMapper _mapper;

        public FeaturesController(ViettelSolutionDbConext context, IMapper mapper, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IEnumerable<FeatureDto>> GetFeature()
        {
            var feature = await _context.Features.ToListAsync();
            foreach (var item in feature)
            {
                item.Image = _storageService.GetFileUrl(item.Image);
            }
            var featurefto = _mapper.Map<IEnumerable<FeatureDto>>(feature);
            return featurefto;
        }
        //Get{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<FeatureDto>> GetFeatureById(string id)
        {
            var feature = await _context.Features.FindAsync(id);

            if (feature == null)
            {
                return NotFound();
            }
            feature.Image = _storageService.GetFileUrl(feature.Image);
            var featuredto = _mapper.Map<FeatureDto>(feature);
            return featuredto;
        }
        [HttpGet("GetFeatureBySolution/{solutionId}")]
        public async Task<IEnumerable<FeatureDto>> GetFeatureBySolution(string solutionId)
        {
            var features = await _context.Features
                .Include(p => p.Solution)
                .Where(p => p.SolutionId.Equals(solutionId))
                .AsNoTracking()
                .ToListAsync();

            foreach (var item in features)
            {
                item.Image = _storageService.GetFileUrl(item.Image);
            }

            var featuredto = _mapper.Map<IEnumerable<FeatureDto>>(features);

            return featuredto;
        }

        //Create
        [HttpPost]
        public async Task<ActionResult<FeatureDto>> PostFeature([FromForm] FeatureCreateRequest featureCreateRequest)
        {
            var feature = _mapper.Map<Feature>(featureCreateRequest);
            feature.Id = Guid.NewGuid().ToString();

            if (featureCreateRequest.ThumbnailImages != null)
            {
                feature.Image = await SaveFile(featureCreateRequest.ThumbnailImages);
            }
            else
            {
                feature.Image = "noimage.png";
            }

            _context.Features.Add(feature);
            await _context.SaveChangesAsync();

            var featuredto = _mapper.Map<FeatureDto>(feature);
            return featuredto;
        }
        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult<FeatureDto>> PutFeature(string id, [FromForm] FeatureCreateRequest featureCreateRequest)
        {
            var feature = await _context.Features.FindAsync(id);

            if (feature == null)
            {
                return NotFound();
            }

            if (featureCreateRequest.ThumbnailImages != null)
            {
                feature.Image = await SaveFile(featureCreateRequest.ThumbnailImages);
            }

            _context.Entry<Feature>(feature).CurrentValues.SetValues(featureCreateRequest);

            await _context.SaveChangesAsync();

            var featuredto = _mapper.Map<FeatureDto>(feature);

            return featuredto;
        }
        //Delete
        [HttpDelete("{id}")]
        public async Task<ActionResult<FeatureDto>> DeleteFeature(string id)
        {
            var feature = await _context.Features.FindAsync(id);
            if (feature == null)
            {
                return NotFound();
            }
            var featuredto = _mapper.Map<FeatureDto>(feature);
            _context.Features.Remove(feature);
            await _context.SaveChangesAsync();

            return featuredto;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}
