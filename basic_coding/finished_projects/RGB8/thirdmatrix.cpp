#include <iostream>

using namespace std;

int main(){
	int n, i, j;
	cin >> n;
	
	int A[n][n], B[n][n];
	for (i = 0; i < n; i++)
		for (j = 0; j < n; j++)
			cin >> A[i][j];
	for (i = 0; i < n; i++)
		for (j = 0; j < n; j++)
			cin >> B[i][j];
	
	for (i = 0; i < n; i++){
		for (j = 0; j < n; j++)
			cout << A[i][j] + B[i][j] << ' ';
		cout << endl;
	}
	return 0;
}
