#include <iostream>

using namespace std;

int main(){
	
	int n, i, j, sum1 = 0, sum2 = 0;
	cin >> n;
	int A[n][n];
	
	for (i = 0; i < n; i++)
		for (j = 0; j < n; j++)
			cin >> A[i][j];
			
	for (i = 0; i < n; i++)
		sum1 += A[i][i];
		
	for (i = n - 1, j = 0; i >= 0; i--, j++)
			sum2 += A[i][j];
			
	cout << sum1 << ' ' << sum2 << endl;
	return 0;
}
