using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using System.IO;

[System.Serializable]
public class TrainingSet
{
	public double[] input;
	public double output;
}

public class Perceptron : MonoBehaviour {

	public List<TrainingSet> ts = new List<TrainingSet>();
	double[] weights = {0,0};
	double bias = 0;
	double totalError = 0;
	public GameObject ethan;

	double DotProductBias(double[] v1, double[] v2) 
	{
		if (v1 == null || v2 == null)
			return -1;
	 
		if (v1.Length != v2.Length)
			return -1;
	 
		double d = 0;
		for (int x = 0; x < v1.Length; x++)
		{
			d += v1[x] * v2[x];
		}

		d += bias;
	 
		return d;
	}

	double CalcOutput(int i)
	{
		return(ActivationFunction(DotProductBias(weights,ts[i].input)));
	}

	double CalcOutput(double i1, double i2)
	{
		double[] inp = new double[] {i1, i2};
		return(ActivationFunction(DotProductBias(weights,inp)));
	}

	double ActivationFunction(double dp)
	{
		if(dp > 0) return (1);
		return(0);
	}

	void InitialiseWeights()
	{
		for(int i = 0; i < weights.Length; i++)
		{
			weights[i] = Random.Range(-1.0f,1.0f);
		}
		bias = Random.Range(-1.0f,1.0f);
	}

	void UpdateWeights(int j)
	{
		double error = ts[j].output - CalcOutput(j);
		totalError += Mathf.Abs((float)error);
		for(int i = 0; i < weights.Length; i++)
		{			
			weights[i] = weights[i] + error*ts[j].input[i]; 
		}
		bias += error;
	}

	void Train()
	{
		for(int t = 0; t < ts.Count; t++)
		{
			UpdateWeights(t);
			Debug.Log("W1: " + (weights[0]) + " W2: " + (weights[1]) + " B: " + bias);
		}	
	}


	void Start () {
		InitialiseWeights();
	}

	public void getinput(double i1 ,double i2,double op)
	{
		double output = CalcOutput(i1, i2);
		if(output==0)
		{
			ethan.GetComponent<Animator>().SetTrigger("Crouch");
			ethan.GetComponent<Rigidbody>().isKinematic = false;
		}
		else
		{
			ethan.GetComponent<Rigidbody>().isKinematic = true;
		}
		TrainingSet train = new TrainingSet(); 
		double[] ip = { i1, i2 };
		double opp = op;
		train.input = ip;
		train.output = opp;
		ts.Add(train);
		Train();
		
	}
	public void load()
	{
		string path = Application.dataPath + "/weights.txt";
		if(File.Exists(path))
		{
			var sr = File.OpenText(path);
			string line = sr.ReadLine();
			string[] w = line.Split(',');
			weights[0] = System.Convert.ToDouble(w[0]);
			weights[1] = System.Convert.ToDouble(w[1]);
			bias = System.Convert.ToDouble(w[2]);
			Debug.Log("Loading");
		}
	}
	public void save()
	{
		string path = Application.dataPath + "/weights.txt";
		var sr = File.CreateText(path);
		sr.WriteLine(weights[0] + "," + weights[1] + "," + bias);
		sr.Close();
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.S))
		{
			save();
		}
		if(Input.GetKeyDown(KeyCode.L))
		{
			load();
		}
	}
}