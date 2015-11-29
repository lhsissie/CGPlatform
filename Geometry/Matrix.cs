﻿using System;

namespace Geometry
{
	public class Matrix3d
	{
		private const int M = 3, N = 3, length = 9;

		private double[] arr = new double[length];

		public Matrix3d()
		{
			for (int i = 0; i < length; ++i)
			{
				arr[i] = 0;
			}
		}

		public Matrix3d(double[] array, bool rowWise = true)
		{
			if (rowWise)
			{
				for (int i = 0; i < length; ++i)
				{
					arr[i] = array[i];
				}
			}
			else //col-wise
			{
				for (int i = 0; i < M; ++i)
				{
					for (int j = 0; j < N; ++j)
					{
						arr[i * N + j] = array[j * M + i];
					}
				}
			}
		}

		public Matrix3d(double[,] array)
		{
			for (int i = 0; i < M; ++i)
			{
				for (int j = 0; j < N; ++j)
				{
					arr[i * N + j] = array[i, j];
				}
			}
		}

		public double this[int row, int col]
		{
			get
			{
				if (row >= 0 && row < M && col >= 0 && col < N)
				{
					return arr[row * N + col];
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
			set
			{
				if (row >= 0 && row < M && col >= 0 && col < N)
				{
					arr[row * N + col] = value;
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public double this[int index]
		{
			get
			{
				if (index >= 0 && index < length)
				{
					return arr[index];
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
			set 
			{
				if (index >= 0 && index < length)
				{
					arr[index] = value;
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public Matrix3d(Matrix3d mat)
		{
			for (int i = 0; i < length; ++i)
			{
				arr[i] = mat[i];
			}
		}

        public double[] toArray()
        {
            return arr;
        }

		// operators
		static public Matrix3d operator +(Matrix3d m1, Matrix3d m2)
		{
			Matrix3d m = new Matrix3d(m1);
			for (int i = 0; i < length; ++i)
			{
				m[i] += m2[i];
			}
			return m;
		}

		static public Matrix3d operator -(Matrix3d m1, Matrix3d m2)
		{
			Matrix3d m = new Matrix3d(m1);
			for (int i = 0; i < length; ++i)
			{
				m[i] -= m2[i];
			}
			return m;
		}

		static public Matrix3d operator *(double factor, Matrix3d m)
		{
			Matrix3d mat = new Matrix3d(m);
			for (int i = 0; i < length; ++i)
			{
				mat[i] *= factor;
			}
			return mat;
		}

        static public Matrix3d operator *(Matrix3d m1, Matrix3d m2)
        {
            Matrix3d mat = new Matrix3d();
            for (int i = 0; i < M; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    for (int k = 0; k < N; ++k)
                    {
                        mat[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return mat;
        }

		static public Matrix3d operator /(Matrix3d m, double factor)
		{
			Matrix3d mat = new Matrix3d(m);
			if (Math.Abs(factor) < 1e-6) 
			{
				throw new DivideByZeroException();
			}
			for (int i = 0; i < length; ++i)
			{
				mat[i] /= factor;
			}
			return mat;
		}

		// numerics
		public static Matrix3d IdentityMatrix()
		{
			Matrix3d mat = new Matrix3d();
			mat[0, 0] = mat[1, 1] = mat[2, 2] = 1.0;
			return mat;
		}

		public Matrix3d Transpose()
		{
			Matrix3d mat = new Matrix3d();
			for (int i = 0; i < M; ++i)
			{
				for (int j = 0; j < N; ++j)
				{
					mat[j, i] = this[i, j];
				}
			}
			return mat;
		}

		public double Trace()
		{
			return this[0, 0] + this[1, 1] + this[2, 2];
		}

		public double Determinant()
		{
			return this[0, 0] * (this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1])
				- this[0, 1] * (this[1, 0] * this[2, 2] - this[1, 2] * this[2, 0])
				+ this[0, 2] * (this[1, 0] * this[2, 1] - this[1, 1] * this[2, 0]);
		}

		public Matrix3d Inverse()
		{
			Matrix3d mat = new Matrix3d();
			double det = this.Determinant();
			if (det == 0) throw new DivideByZeroException("Determinant equals to 0!");
			mat[0, 0] = this[1, 1] * this[2, 2] - this[1, 2] * this[2, 1];
			mat[0, 1] = this[0, 2] * this[2, 1] - this[0, 1] * this[2, 2];
			mat[0, 2] = this[0, 1] * this[1, 2] - this[0, 2] * this[1, 1];
			mat[1, 0] = this[1, 2] * this[2, 0] - this[1, 0] * this[2, 2];
			mat[1, 1] = this[0, 0] * this[2, 2] - this[0, 2] * this[2, 0];
			mat[1, 2] = this[0, 2] * this[1, 0] - this[0, 0] * this[1, 2];
			mat[2, 0] = this[1, 0] * this[2, 1] - this[1, 1] * this[2, 0];
			mat[2, 1] = this[0, 1] * this[2, 0] - this[0, 0] * this[2, 1];
			mat[2, 2] = this[0, 0] * this[1, 1] - this[0, 1] * this[1, 0];
			mat = mat / det;
			return mat;
		}

	}//class-Matrix3d

	public class Matrix4d
	{
		private const int M = 4, N = 4, length = 16;

		private double[] arr = new double[length];

		public Matrix4d()
		{
			for (int i = 0; i < length; ++i)
			{
				arr[i] = 0;
			}
		}

		public Matrix4d(double[] array, bool rowWise = true)
		{
			if (rowWise)
			{
				for (int i = 0; i < length; ++i)
				{
					arr[i] = array[i];
				}
			}
			else //col-wise
			{
				for (int i = 0; i < M; ++i)
				{
					for (int j = 0; j < N; ++j)
					{
						arr[i * N + j] = array[j * M + i];
					}
				}
			}
		}

		public Matrix4d(double[,] array)
		{
			for (int i = 0; i < M; ++i)
			{
				for (int j = 0; j < N; ++j)
				{
					arr[i * N + j] = array[i, j];
				}
			}
		}

		public double this[int row, int col]
		{
			get
			{
				if (row >= 0 && row < M && col >= 0 && col < N)
				{
					return arr[row * N + col];
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
			set
			{
				if (row >= 0 && row < M && col >= 0 && col < N)
				{
					arr[row * N + col] = value;
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public double this[int index]
		{
			get
			{
				if (index >= 0 && index < length)
				{
					return arr[index];
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
			set
			{
				if (index >= 0 && index < length)
				{
					arr[index] = value;
				}
				else
				{
					throw new ArgumentOutOfRangeException();
				}
			}
		}

		public Matrix4d(Matrix4d mat)
		{
			for (int i = 0; i < length; ++i)
			{
				arr[i] = mat[i];
			}
		}

        public double[] toArray()
        {
            return arr;
        }

		// operators
		static public Matrix4d operator +(Matrix4d m1, Matrix4d m2)
		{
			Matrix4d m = new Matrix4d(m1);
			for (int i = 0; i < length; ++i)
			{
				m[i] += m2[i];
			}
			return m;
		}

		static public Matrix4d operator -(Matrix4d m1, Matrix4d m2)
		{
			Matrix4d m = new Matrix4d(m1);
			for (int i = 0; i < length; ++i)
			{
				m[i] -= m2[i];
			}
			return m;
		}

		static public Matrix4d operator *(double factor, Matrix4d m)
		{
			Matrix4d mat = new Matrix4d(m);
			for (int i = 0; i < length; ++i)
			{
				mat[i] *= factor;
			}
			return mat;
		}

        static public Matrix4d operator *(Matrix4d m1, Matrix4d m2)
        {
            Matrix4d mat = new Matrix4d();
            for (int i = 0; i < M; ++i)
            {
                for (int j = 0; j < N; ++j)
                {
                    for (int k = 0; k < N; ++k)
                    {
                        mat[i, j] += m1[i, k] * m2[k, j];
                    }
                }
            }
            return mat;
        }

		static public Matrix4d operator /(double factor, Matrix4d m)
		{
			Matrix4d mat = new Matrix4d(m);
			if (factor == 0) throw new DivideByZeroException();
			for (int i = 0; i < length; ++i)
			{
				mat[i] /= factor;
			}
			return mat;
		}


		// numerics
		public static Matrix4d IdentityMatrix()
		{
			Matrix4d mat = new Matrix4d();
			mat[0, 0] = mat[1, 1] = mat[2, 2] = mat[3, 3] = 1.0;
			return mat;
		}

		public Matrix4d Transpose()
		{
			Matrix4d mat = new Matrix4d();
			for (int i = 0; i < M; ++i)
			{
				for (int j = 0; j < N; ++j)
				{
					mat[j, i] = this[i, j];
				}
			}
			return mat;
		}

		public double Trace()
		{
			return this[0, 0] + this[1, 1] + this[2, 2] + this[3, 3];
		}

		public double Determinant()
		{
			return this[0, 0] * FormMatrix3D(0, 0).Determinant()
				- this[0, 1] * FormMatrix3D(0, 1).Determinant()
				+ this[0, 2] * FormMatrix3D(0, 2).Determinant()
				- this[0, 3] * FormMatrix3D(0, 3).Determinant();
		}

		public Matrix3d FormMatrix3D(int row, int col)
		{
			// remove the element at [row, col]
			// for calculating the determinant
			Matrix3d mat = new Matrix3d();
			int r = 0;
			for (int i = 0; i < M; ++i)
			{
				if(i == row) continue;
				int c = 0;
				for (int j = 0; j < N; ++j)
				{
					if (j == col) continue;
					mat[r, c++] = this[i, j];
				}
				++r;
			}
			return mat;
		}

		// transformation
        public static Matrix4d TranslationMatrix(Vector3d v)
		{
			Matrix4d mat = IdentityMatrix();
			for (int i = 0; i < 3; ++i)
			{
				mat[i, 3] = v[i];
			}
			return mat;
		}

        public static Matrix4d ScalingMatrix(Vector3d v)
		{
			Matrix4d mat = IdentityMatrix();
			for (int i = 0; i < 3; ++i)
			{
				mat[i, i] = v[i];
			}
			return mat;
		}

		public static Matrix4d RotationMatrix(Vector3d axis, double angle)
		{
			Matrix4d mat = new Matrix4d();

			double cos = Math.Cos(angle);
			double sin = Math.Sin(angle);

			axis.Normalize();
			double x = axis[0], y = axis[1], z = axis[2];

			mat[0, 0] = cos + x * x * (1 - cos);
			mat[0, 1] = x * y * (1 - cos) - z * sin;
			mat[0, 2] = x * z * (1 - cos) + y * sin;
			mat[1, 0] = y * x * (1 - cos) + z * sin;
			mat[1, 1] = cos + y * y * (1 - cos);
			mat[1, 2] = y * z * (1 - cos) - x * sin;
			mat[2, 0] = z * x * (1 - cos) - y * sin;
			mat[2, 1] = z * y * (1 - cos) + x * sin;
			mat[2, 2] = cos + z * z * (1 - cos);
			mat[3, 3] = 1;

			return mat;
		}

	}//class-Matrix4d

	public class MatrixNd
	{
		private int M, N, length;

		private double[] arr;

		public int Row
		{
			get
			{
				return this.M;
			}
		}

		public int Col
		{
			get
			{
				return this.N;
			}
		}

		public MatrixNd(int nr, int nc)
		{
			M = nr;
			N = nc;
			length = M * N;
			arr = new double[length];
			for (int i = 0; i < length; ++i)
			{
				arr[i] = 0;
			}
		}

		public MatrixNd(int nr, int nc, double[] array)
		{
			M = nr;
			N = nc;
			length = M * N;
			arr = new double[length];
			for (int i = 0; i < length; ++i)
			{
				arr[i] = array[i];
			}
		}

		public MatrixNd(MatrixNd mat)
		{
			M = mat.Row;
			N = mat.Col;
			length = M * N;
			arr = new double[length];
			for (int i = 0; i < length; ++i)
			{
				arr[i] = mat.arr[i];
			}
		}

		public double this[int row, int col]
		{
			get
			{
				if (row >= 0 && row < M && col >= 0 && col < N)
					return arr[row * N + col];
				else
					throw new ArgumentOutOfRangeException();
			}
			set
			{
				if (row >= 0 && row < M && col >= 0 && col < N)
					arr[row * N + col] = value;
				else
					throw new ArgumentOutOfRangeException();
			}
		}

		public double this[int index]
		{
			get
			{
				if (index >= 0 && index < length)
					return arr[index];
				else
					throw new ArgumentOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < length)
					arr[index] = value;
				else
					throw new ArgumentOutOfRangeException();
			}
		}

        public double[] toArray()
        {
            return arr;
        }

		// operators
		static public MatrixNd operator +(MatrixNd m1, MatrixNd m2)
		{
			if(m1.M != m2.M || m1.N != m2.N)
			{
				return null;
			}
			MatrixNd m = new MatrixNd(m1);
			for (int i = 0; i < m1.length; ++i)
			{
				m[i] += m2[i];
			}
			return m;
		}

		static public MatrixNd operator -(MatrixNd m1, MatrixNd m2)
		{
			if (m1.M != m2.M || m1.N != m2.N)
			{
				return null;
			}
			MatrixNd m = new MatrixNd(m1);
			for (int i = 0; i < m1.length; ++i)
			{
				m[i] -= m2[i];
			}
			return m;
		}

		static public MatrixNd operator *(double factor, MatrixNd m)
		{
			MatrixNd mat = new MatrixNd(m);
			for (int i = 0; i < m.length; ++i)
			{
				mat[i] *= factor;
			}
			return mat;
		}

		static public MatrixNd operator /(double factor, MatrixNd m)
		{
			MatrixNd mat = new MatrixNd(m);
			if (factor == 0) throw new DivideByZeroException();
			for (int i = 0; i < m.length; ++i)
			{
				mat[i] /= factor;
			}
			return mat;
		}

		// numerics
		public MatrixNd IdentityMatrix(int r, int c)
		{
			MatrixNd mat = new MatrixNd(r, c);
			for (int i = 0; i < r;++i )
			{
				mat[i, i] = 1.0;
			}
			return mat;
		}

		public MatrixNd Transpose()
		{
			MatrixNd mat = new MatrixNd(N, M);
			for (int i = 0; i < M; ++i)
			{
				for (int j = 0; j < N; ++j)
				{
					mat[j, i] = this[i, j];
				}
			}
			return mat;
		}

		public double Trace()
		{
			double sum = 0.0;
			int n = M < N ? M : N;
			for (int i = 0; i < n; ++i)
			{
				sum += this[i, i];
			}
			return sum;
		}

	}//class-MatrixNd
}
