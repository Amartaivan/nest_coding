#include <iostream>

using namespace std;

int main(){
	
	int n, m, k, i, j, x, y, result = 0;
	cin >> n >> m;
	int A[n][m];
	
	for (i = 0; i < n; i++)
		for (j = 0; j < m; j++)
			cin >> A[i][j];
	
	cin >> k;
	for (i = 0; i < k; i++){
		cin >> x >> y;
		result += A[x - 1][y - 1];
	}
	
	cout << result << endl;
	return 0;
}
