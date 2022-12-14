#if UDONSHARP
using UdonSharp;
#endif
using System;
using UnityEngine;

namespace anatawa12.RealStarSkybox
{
    public class ShaderRotationSetter :
#if UDONSHARP
        UdonSharpBehaviour
#else
        MonoBehaviour
#endif
    {
        private DateTime _nextUpdate;
        public float latitude;
        public float northDirection;
        private readonly int _rotQuot = Shader.PropertyToID("_RotQuot");

        private void Update()
        {
            var now = DateTime.Now;
            if (_nextUpdate > now) return;
            // when you modify this logic, you also must update SkyboxCubemapRotatableShaderGUI.
            var initial = Quaternion.Euler(90, 180, 0);
            var skybox = RenderSettings.skybox;
            var truncatedJulianDay = CalcTruncatedJulianDay(now);
            var time = CalcSiderealTime(truncatedJulianDay);
            var rotation = time * -15 % 360 + 180;
            var quot = Quaternion.Euler(latitude, northDirection, (float)rotation);
            quot *= initial;
            skybox.SetVector(_rotQuot, new Vector4(quot.x, quot.y, quot.z, quot.w));
            _nextUpdate = now + TimeSpan.FromSeconds(1);
            Debug.Log("it's " + now + " (" + truncatedJulianDay + ") so rotating " + rotation + "deg");
        }

        private static double CalcSiderealTime(double truncatedJulianDay)
        {
            return 24 * (0.671_262 + 1.002_737_9094 * truncatedJulianDay);
        }

        // see https://ja.wikipedia.org/wiki/%E6%81%92%E6%98%9F%E6%99%82
        private static double CalcTruncatedJulianDay(DateTime time)
        {
            var y = time.Year;
            var m = time.Month;
            if (m <= 2)
            {
                y -= 1;
                m += 12;
            }

            return Math.Floor(y * 365.25)
                   + Math.Floor(y / 400.0)
                   - Math.Floor(y / 100.0)
                   + Math.Floor(30.59 * (m - 2))
                   + time.Day
                   + 1_721_088.5
                   + time.Hour / 24.0
                   + time.Minute / 1440.0
                   + time.Second / 86_400.0
                   - 2_440_000.5;
        }
    }
}
