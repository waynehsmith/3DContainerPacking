using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using CromulentBisgetti.ContainerPacking;

namespace CromulentBisgetti.ContainerPacking.Entities
{
    /// <summary>
    /// An item to be packed. Also used to hold post-packing details for the item.
    /// </summary>
    [DataContract]
	[DebuggerDisplay("CPE:Item {DisplayValue(),nq}")]
	public class Item
	{
		#region Private Variables

		private decimal volume;

		#endregion Private Variables

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the Item class.
		/// </summary>
		/// <param name="id">The item ID.</param>
		/// <param name="dim1">The length of one of the three item dimensions.</param>
		/// <param name="dim2">The length of another of the three item dimensions.</param>
		/// <param name="dim3">The length of the other of the three item dimensions.</param>
		/// <param name="itemQuantity">The item quantity.</param>
		public Item(int id, decimal dim1, decimal dim2, decimal dim3, int quantity)
		{
			this.ID = id;
			this.Dim1 = dim1;
			this.Dim2 = dim2;
			this.Dim3 = dim3;
			this.volume = dim1 * dim2 * dim3;
			this.Quantity = quantity;
		}

		#endregion Constructors

		#region Public Properties

		/// <summary>
		/// Gets or sets the item ID.
		/// </summary>
		/// <value>
		/// The item ID.
		/// </value>
		[DataMember]
		public int ID { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this item has already been packed.
		/// </summary>
		/// <value>
		///   True if the item has already been packed; otherwise, false.
		/// </value>
		[DataMember]
		public bool IsPacked { get; set; }

		/// <summary>
		/// Gets or sets the length of one of the item dimensions.
		/// </summary>
		/// <value>
		/// The first item dimension.
		/// </value>
		[DataMember]
		public decimal Dim1 { get; set; }

		/// <summary>
		/// Gets or sets the length another of the item dimensions.
		/// </summary>
		/// <value>
		/// The second item dimension.
		/// </value>
		[DataMember]
		public decimal Dim2 { get; set; }

		/// <summary>
		/// Gets or sets the third of the item dimensions.
		/// </summary>
		/// <value>
		/// The third item dimension.
		/// </value>
		[DataMember]
		public decimal Dim3 { get; set; }

		/// <summary>
		/// Gets or sets the x coordinate of the location of the packed item within the container.
		/// </summary>
		/// <value>
		/// The x coordinate of the location of the packed item within the container.
		/// </value>
		[DataMember]
		public decimal CoordX { get; set; }

		/// <summary>
		/// Gets or sets the y coordinate of the location of the packed item within the container.
		/// </summary>
		/// <value>
		/// The y coordinate of the location of the packed item within the container.
		/// </value>
		[DataMember]
		public decimal CoordY { get; set; }

		/// <summary>
		/// Gets or sets the z coordinate of the location of the packed item within the container.
		/// </summary>
		/// <value>
		/// The z coordinate of the location of the packed item within the container.
		/// </value>
		[DataMember]
		public decimal CoordZ { get; set; }

		/// <summary>
		/// Gets or sets the item quantity.
		/// </summary>
		/// <value>
		/// The item quantity.
		/// </value>
		public int Quantity { get; set; }

		/// <summary>
		/// Gets or sets the x dimension of the orientation of the item as it has been packed.
		/// </summary>
		/// <value>
		/// The x dimension of the orientation of the item as it has been packed.
		/// </value>
		[DataMember]
		public decimal PackDimX { get; set; }

		/// <summary>
		/// Gets or sets the y dimension of the orientation of the item as it has been packed.
		/// </summary>
		/// <value>
		/// The y dimension of the orientation of the item as it has been packed.
		/// </value>
		[DataMember]
		public decimal PackDimY { get; set; }

		/// <summary>
		/// Gets or sets the z dimension of the orientation of the item as it has been packed.
		/// </summary>
		/// <value>
		/// The z dimension of the orientation of the item as it has been packed.
		/// </value>
		[DataMember]
		public decimal PackDimZ { get; set; }

		/// <summary>
		/// Gets the item volume.
		/// </summary>
		/// <value>
		/// The item volume.
		/// </value>
		[DataMember]
		public decimal Volume 
		{
			get
			{
				return volume;
			}
		}

		#endregion Public Properties

		[Flags]
		public enum FormatFlags
        {
			None			= 0,
			Summary			= 1 << 0,
			Dimensions		= 1 << 1,
			Coordinates		= 1 << 2,
			Packed			= 1 << 3,

			Formatted		= 1 << 7,
			
			Container		= Coordinates | Packed,
			General			= Dimensions | Container,			
			All				= Summary | General

        }

        #region Methods
        public override string ToString()
		{
			return DisplayValue(FormatFlags.General);
		}
		public string DisplayValue(FormatFlags flags = FormatFlags.All, bool forceFormatted = false)
		{
			var builder = new System.Text.StringBuilder();
			if (forceFormatted)
            {
				flags = flags | FormatFlags.Formatted;
            }
			bool showPacked = (PackDimX > 0 || PackDimY > 0 | PackDimZ > 0);

			builder.Append($"ID:{ID}\tQty:{Quantity}\t");
            if (flags.HasFlag(FormatFlags.Summary))
            {
				builder.Append($"Volume:{Volume}");
				if (showPacked)
				{
					builder.Append($" Packed");
				}
				builder.Append("\n\t");

			}

			if (flags.HasFlag(FormatFlags.Coordinates) && showPacked)
			{
				builder.Append("Coords->");
				builder.Append(PackingUtilities.CreateTriplet(
					CoordX, CoordY, CoordZ) + "\t");
			}

			if (flags.HasFlag(FormatFlags.Dimensions))
			{
				builder.Append("Dimensions->");
				builder.Append(PackingUtilities.CreateTriplet(
					Dim1, Dim2, Dim3) + "\t");
			}

			if (flags.HasFlag(FormatFlags.Packed) && showPacked)
			{
				builder.Append("Packed->");
				builder.Append(PackingUtilities.CreateTriplet(
					PackDimX, PackDimY, PackDimZ) + "\n");
			}
			if (!flags.HasFlag(FormatFlags.Formatted))
            {
				builder.Replace("\t", " ");
				builder.Replace("\n", "");
			}
			return builder.ToString();
		}
		public override bool Equals(object obj)
		{
			if (!(obj is Item))
				return false;

			Item target = (Item)obj;
			return (
				ID == target.ID &&
				IsPacked == target.IsPacked &&
				Quantity == target.Quantity &&
				Volume == target.Volume &&

				Dim1 == target.Dim1 &&
				Dim2 == target.Dim2 &&
				Dim3 == target.Dim3 &&

				CoordX == target.CoordX &&
				CoordY == target.CoordY &&
				CoordZ == target.CoordZ &&

				PackDimX == target.PackDimX &&
				PackDimY == target.PackDimY &&
				PackDimZ == target.PackDimZ 
				);
		}

		public override int GetHashCode()
		{
			return PackingUtilities.CreateHashCode(new int[]
			{
				ID.GetHashCode(),
				IsPacked.GetHashCode(),
				Quantity.GetHashCode(),
				Volume.GetHashCode(),

				Dim1.GetHashCode(),
				Dim2.GetHashCode(),
				Dim3.GetHashCode(),

				CoordX.GetHashCode(),
				CoordY.GetHashCode(),
				CoordZ.GetHashCode(),

				PackDimX.GetHashCode(),
				PackDimY.GetHashCode(),
				PackDimZ.GetHashCode(),
			});			
		}
		#endregion
	}
}
