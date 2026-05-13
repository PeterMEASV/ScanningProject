using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ScanController(ILogger<ScanController> logger) : ControllerBase
{
    private static readonly string FramesDir = Path.Combine(Path.GetTempPath(), "frames");

    [HttpPost]
    [Consumes("image/jpeg", "application/octet-stream")]
    public async Task<IActionResult> ReceiveFrame()
    {
        using var ms = new MemoryStream();
        await Request.Body.CopyToAsync(ms);
        var frameBytes = ms.ToArray();

        if (frameBytes.Length == 0)
            return BadRequest("Empty frame");

        Directory.CreateDirectory(FramesDir);
        var filename = $"{DateTime.UtcNow:yyyyMMdd_HHmmss_fff}.jpg";
        await System.IO.File.WriteAllBytesAsync(Path.Combine(FramesDir, filename), frameBytes);

        logger.LogInformation("Frame saved: {File} ({Bytes} bytes)", filename, frameBytes.Length);

        return Ok(new { receivedBytes = frameBytes.Length, receivedAt = DateTime.UtcNow, filename });
    }

    [HttpGet("latest")]
    public IActionResult GetLatest()
    {
        if (!Directory.Exists(FramesDir))
            return NotFound("No frames received yet");

        var latest = Directory.GetFiles(FramesDir, "*.jpg")
            .OrderDescending()
            .FirstOrDefault();

        if (latest is null)
            return NotFound("No frames received yet");

        return PhysicalFile(latest, "image/jpeg");
    }
}
