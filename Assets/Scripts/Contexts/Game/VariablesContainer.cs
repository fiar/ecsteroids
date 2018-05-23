using System;

namespace Scripts.Contexts.Game
{
	// Very simple static class, to store variables
	public static class VariablesContainer
	{
		public static event Action<int> ScoreChanged;
		public static event Action<float> LaserEnergyChanged;

		private static int _score;
		private static float _laserEnergy;

		public static void Reset()
		{
			_score = 0;
			_laserEnergy = 1f;
		}

		public static int Score
		{
			get { return _score; }
			set
			{
				if (_score != value)
				{
					_score = value;
					ScoreChanged?.Invoke(_score);
				}
			}
		}

		public static float LaserEnergy
		{
			get { return _laserEnergy; }
			set
			{
				if (_laserEnergy != value)
				{
					_laserEnergy = value;
					LaserEnergyChanged?.Invoke(_laserEnergy);
				}
			}
		}
	}
}