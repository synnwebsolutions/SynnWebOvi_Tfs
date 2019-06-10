/*******************************************************************************
 *
 * License :
 *
 *  SoundTouch audio processing library
 *  Copyright (c) Olli Parviainen
 *  C# port Copyright (c) Olaf Woudenberg
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 2.1 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 ******************************************************************************/

using System;

namespace SoundStretch
{
    public class RunParameters
    {
        private void CheckLimits()
        {
            if (TempoDelta < -95.0f)
            {
                TempoDelta = -95.0f;
            }
            else if (TempoDelta > 5000.0f)
            {
                TempoDelta = 5000.0f;
            }

            if (PitchDelta < -60.0f)
            {
                PitchDelta = -60.0f;
            }
            else if (PitchDelta > 60.0f)
            {
                PitchDelta = 60.0f;
            }

            if (RateDelta < -95.0f)
            {
                RateDelta = -95.0f;
            }
            else if (RateDelta > 5000.0f)
            {
                RateDelta = 5000.0f;
            }
        }

        public float TempoDelta { get; set; }
        public float PitchDelta { get; set; }
        public float RateDelta { get; set; }

        public int Quick = 0;
        public int NoAntiAlias = 0;

        public RunParameters()
        {
            TempoDelta = 0;
            PitchDelta = 0;
            RateDelta = 0;
            Quick = 0;
            NoAntiAlias = 0;
            CheckLimits();
        }
    }
}