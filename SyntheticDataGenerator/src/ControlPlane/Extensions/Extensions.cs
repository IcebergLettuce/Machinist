
namespace ControlPlane;

public static class Extensions
{
    public static string SanitizeTickerFormat(this string ticker)
    {
        if ((ticker.Length > 5) || (ticker.Length < 3))
            throw new HttpResponseException(400, "Ticker is too long or too short.");

        return ticker.ToUpper();
    }
}
