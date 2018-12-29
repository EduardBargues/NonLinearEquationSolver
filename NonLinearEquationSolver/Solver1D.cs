using System;

namespace Solver1D {
    public static class Solver1D {
        public static double? BisectionFindRoot ( Func<double , double> f , double lowerBound , double upperBound ,
            double rangeTolerance = 1e-10 , double fTolerance = 1e-3 , int maxIterations = 100 ) {
            double root = 0.5 * ( upperBound + lowerBound );
            bool convergence = false;
            double fLowerBound = f ( lowerBound );
            for ( int iteration = 0 ; iteration < maxIterations && !convergence ; iteration++ ) {
                double fRoot = f ( root );
                double range = Math.Abs ( upperBound - lowerBound );
                convergence = range <= rangeTolerance && Math.Abs ( fRoot ) <= fTolerance;
                if ( Math.Sign ( fRoot ) == Math.Sign ( fLowerBound ) ) {
                    lowerBound = root;
                    fLowerBound = fRoot;
                } else {
                    upperBound = root;
                }
                root = 0.5 * ( upperBound + lowerBound );
            }

            return convergence ? root : ( double? ) null;
        }

        public static double? SecantFindRoot ( Func<double , double> f , double lowerBound , double upperBound ,
            double rangeTolerance = 1e-10 , double fTolerance = 1e-3 , int maxIterations = 100 ) {
            double fLowerBound = f ( lowerBound );
            double fUpperBound = f ( upperBound );
            double root = upperBound - ( upperBound - lowerBound ) / ( fUpperBound - fLowerBound ) * fUpperBound;
            bool convergence = false;
            for ( int iteration = 0 ; iteration < maxIterations && !convergence ; iteration++ ) {
                double fRoot = f ( root );
                double range = Math.Min ( upperBound - root , root - lowerBound );
                convergence = range <= rangeTolerance && Math.Abs ( fRoot ) <= fTolerance;
                int fRootSign = Math.Sign ( fRoot );
                if ( fRootSign == Math.Sign ( fLowerBound ) ) {
                    lowerBound = root;
                    fLowerBound = fRoot;
                } else if ( fRootSign == Math.Sign ( fUpperBound ) ) {
                    upperBound = root;
                    fUpperBound = fRoot;
                } else {
                    if ( Math.Abs ( fLowerBound ) > Math.Abs ( fUpperBound ) ) {
                        fLowerBound = 0;
                        lowerBound = root;
                    } else {
                        fUpperBound = 0;
                        upperBound = root;
                    }
                }
                root = upperBound - ( upperBound - lowerBound ) / ( fUpperBound - fLowerBound ) * fUpperBound;
            }

            return convergence ? root : ( double? ) null;
        }

        public static double? NewtonRaphson ( Func<double , double> f , Func<double , double> df , double x0 ,
            double xTolerance = 1e-10 , double fTolerance = 1e-10 , int maxIterations = 100 ) {
            bool convergence = false;
            double root = x0;
            for ( int iteration = 0 ; iteration < maxIterations && !convergence ; iteration++ ) {
                double fRoot = f ( root );
                double nextRoot = root - fRoot / df ( root );
                convergence = Math.Abs ( nextRoot - root ) <= xTolerance && Math.Abs ( fRoot ) <= fTolerance;
                root = nextRoot;
            }
            return convergence ? root : ( double? ) null;
        }
    }
}
