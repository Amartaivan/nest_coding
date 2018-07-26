#include <iostream>

using namespace std;

int inline min(int a, int b){
	return a > b ? b : a;
}

int main(){
	
	int n, m, i, j;
	cin >> n >> m;
	int A[n][m];
	
	for (i = 0; i < n; i++)
		for (j = 0; j < m; j++)
			cin >> A[i][j];
		
	for (j = 1; j < m; j++)
		A[0][j] += A[0][j - 1];
	for (i = 1; i < n; i++)
		A[i][0] += A[i - 1][0];

	for (i = 1; i < n; i++)
		for (j = 1; j < m; j++)
			A[i][j] += min(A[i - 1][j], A[i][j - 1]);
		
	cout << A[n - 1][m - 1] << endl;
	return 0;
}
