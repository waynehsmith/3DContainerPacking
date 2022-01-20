using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace CromulentBisgetti.ContainerPacking.Entities
{
	[DataContract]
	[DebuggerDisplay("CPE.AlgorithPackingResult {DisplaySummary(), nq}")]
	public class AlgorithmPackingResult
	{
		#region Constructors

		public AlgorithmPackingResult()
		{
			this.PackedItems = new List<Item>();
			this.UnpackedItems = new List<Item>();
		}

		#endregion Constructors

		#region Public Properties

		[DataMember]
		public int AlgorithmID { get; set; }

		[DataMember]
		public string AlgorithmName { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether all of the items are packed in the container.
		/// </summary>
		/// <value>
		/// True if all the items are packed in the container; otherwise, false.
		/// </value>
		[DataMember]
		public bool IsCompletePack { get; set; }

		/// <summary>
		/// Gets or sets the list of packed items.
		/// </summary>
		/// <value>
		/// The list of packed items.
		/// </value>
		[DataMember]
		public List<Item> PackedItems { get; set; }

		/// <summary>
		/// Gets or sets the elapsed pack time in milliseconds.
		/// </summary>
		/// <value>
		/// The elapsed pack time in milliseconds.
		/// </value>
		[DataMember]
		public long PackTimeInMilliseconds { get; set; }

		/// <summary>
		/// Gets or sets the percent of container volume packed.
		/// </summary>
		/// <value>
		/// The percent of container volume packed.
		/// </value>
		[DataMember]
		public decimal PercentContainerVolumePacked { get; set; }

		/// <summary>
		/// Gets or sets the percent of item volume packed.
		/// </summary>
		/// <value>
		/// The percent of item volume packed.
		/// </value>
		[DataMember]
		public decimal PercentItemVolumePacked { get; set; }

		/// <summary>
		/// Gets or sets the list of unpacked items.
		/// </summary>
		/// <value>
		/// The list of unpacked items.
		/// </value>
		[DataMember]
		public List<Item> UnpackedItems { get; set; }

        #endregion Public Properties

        #region Public Methods
		public string DisplaySummary()
        {
			var builder = new System.Text.StringBuilder();

			builder.Append($"{AlgorithmName} ");

			var pi = PackedItems.Count;
			var up = UnpackedItems.Count;
			builder.Append($"Packed: {pi}/{pi+up} ");

			if (IsCompletePack)
            {
				builder.Append($"Used: {PercentContainerVolumePacked:0}% ");
            }
            else
            {
				builder.Append($"Items: {PercentItemVolumePacked}% ");
			}
			builder.Append($"Time: {PackTimeInMilliseconds}ms ");
			return builder.ToString();
        }

        public override string ToString()
        {
            return DisplaySummary();
        }
        public override bool Equals(object obj)
        {
			if (!(obj is AlgorithmPackingResult))
				return false;

			AlgorithmPackingResult target = (AlgorithmPackingResult)obj;
			return (
				AlgorithmID == target.AlgorithmID
				&& AlgorithmName == target.AlgorithmName
				&& IsCompletePack == target.IsCompletePack
				&& PackTimeInMilliseconds == target.PackTimeInMilliseconds
				&& PercentContainerVolumePacked == target.PercentContainerVolumePacked
				&& PercentItemVolumePacked == target.PercentItemVolumePacked
				&& PackedItems.AreEquivalent(target.PackedItems)
				&& UnpackedItems.AreEquivalent(target.UnpackedItems)
				);
		}
		public override int GetHashCode()
		{
			var components = new int[]
			{
				AlgorithmID.GetHashCode()
				,  AlgorithmName.GetHashCode()
				,  IsCompletePack.GetHashCode()
				,  PackTimeInMilliseconds.GetHashCode()
				,  PercentContainerVolumePacked.GetHashCode()
				,  PercentItemVolumePacked.GetHashCode()
				,  PackedItems.GetEnumerableHashCode()
				,  UnpackedItems.GetEnumerableHashCode()
			};
			return PackingUtilities.CreateHashCode(components);		
		}
		#endregion
	}
}
