﻿// Skeleton implementation written by Joe Zachary for CS 3500, January 2018.
///Remainder of the program written by Bryce Hansen, U0804551, February 2018
/// 

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;

namespace Dependencies
{
    /// <summary>
    /// A DependencyGraph can be modeled as a set of dependencies, where a dependency is an ordered 
    /// pair of strings.  Two dependencies (s1,t1) and (s2,t2) are considered equal if and only if 
    /// s1 equals s2 and t1 equals t2.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that the dependency (s,t) is in DG 
    ///    is called the dependents of s, which we will denote as dependents(s).
    ///        
    ///    (2) If t is a string, the set of all strings s such that the dependency (s,t) is in DG 
    ///    is called the dependees of t, which we will denote as dependees(t).
    ///    
    /// The notations dependents(s) and dependees(s) are used in the specification of the methods of this class.
    ///
    /// For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    ///     dependents("a") = {"b", "c"}
    ///     dependents("b") = {"d"}
    ///     dependents("c") = {}
    ///     dependents("d") = {"d"}
    ///     dependees("a") = {}
    ///     dependees("b") = {"a"}
    ///     dependees("c") = {"a"}
    ///     dependees("d") = {"b", "d"}
    ///     
    /// All of the methods below require their string parameters to be non-null.  This means that 
    /// the behavior of the method is undefined when a string parameter is null.  
    ///
    /// IMPORTANT IMPLEMENTATION NOTE
    /// 
    /// The simplest way to describe a DependencyGraph and its methods is as a set of dependencies, 
    /// as discussed above.
    /// 
    /// However, physically representing a DependencyGraph as, say, a set of ordered pairs will not
    /// yield an acceptably efficient representation.  DO NOT USE SUCH A REPRESENTATION.
    /// 
    /// You'll need to be more clever than that.  Design a representation that is both easy to work
    /// with as well acceptably efficient according to the guidelines in the PS3 writeup. Some of
    /// the test cases with which you will be graded will create massive DependencyGraphs.  If you
    /// build an inefficient DependencyGraph this week, you will be regretting it for the next month.
    /// </summary>
    public class DependencyGraph
    {
        //Two Dictionary objects, to keep track of dependees and dependents.
        private Dictionary<string, HashSet<string>> dependees;
        private Dictionary<string, HashSet<string>> dependents;

        /// <summary>
        /// Creates a DependencyGraph containing no dependencies.
        /// </summary>
        public DependencyGraph()
        {
            //Initialize both dictionaries
            dependees = new Dictionary<string, HashSet<string>>();
            dependents = new Dictionary<string, HashSet<string>>();
        }

        /// <summary>
        /// The number of dependencies in the DependencyGraph.
        /// </summary>
        public int Size
        {
            //invoke help method to find size
            get { return GetSize(); }
        }

        /// <summary>
        /// Helper method to obtain the size of the graph.
        /// </summary>
        /// <returns>int</returns>
        private int GetSize()
        {
            int counter = 0;

            //iterate through each key in one of the maps, and count each valid pair and return it
            foreach (var key in dependees)
                counter += key.Value.Count;

            return counter;
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependents(string s)
        {
            //if the key is in the dependee map, it has dependents.
            if (dependees.ContainsKey(s))
                return true;

            return false;
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.  Requires s != null.
        /// </summary>
        public bool HasDependees(string s)
        {
            //if the key is in the dependent map, it has dependees
            if (dependents.ContainsKey(s))
                return true;

            return false;
        }

        /// <summary>
        /// Enumerates dependents(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependents(string s)
        {
            //looks up hashset in map and returns it
            if (dependees.ContainsKey(s))
                return dependees[s];
            else
                return new HashSet<string>(); // CHANGE TO: return new HashSet<string>();
        }

        /// <summary>
        /// Enumerates dependees(s).  Requires s != null.
        /// </summary>
        public IEnumerable<string> GetDependees(string s)
        {
            //looks up hashset in map and returns it
            if (dependents.ContainsKey(s))
                return dependents[s];
            else
                return new HashSet<string>(); // CHANGE TO: return new HashSet<string>();
        }

        /// <summary>
        /// Adds the dependency (s,t) to this DependencyGraph.
        /// This has no effect if (s,t) already belongs to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>
        public void AddDependency(string s, string t)
        {
            //add new pair to to map, or update existing dependee
            if (dependees.ContainsKey(s))
                dependees[s].Add(t);
            else
                dependees.Add(s, new HashSet<string>(){t});

            //and update dependet graph aswell
            if (dependents.ContainsKey(t))
                dependents[t].Add(s);
            else
                dependents.Add(t, new HashSet<string>() { s });
        }

        /// <summary>
        /// Removes the dependency (s,t) from this DependencyGraph.
        /// Does nothing if (s,t) doesn't belong to this DependencyGraph.
        /// Requires s != null and t != null.
        /// </summary>
        public void RemoveDependency(string s, string t)
        {
            //ADD: if (dependees.Count == 0 || dependents.Count == 0) return;

            //removes the key if located in dependee graph
            if (dependees.ContainsKey(s))
                if (dependees.ContainsKey(s)) //CHANGE TO: dependees[s].Remove(t);
                    dependees[s].Remove(t);    //CHANGE TO: if (dependees[s].Count == 0) dependees.Remove(s);

            //removes the key if located in the dependent graph
            if (dependents.ContainsKey(t))
                if (dependents.ContainsKey(t)) //CHANGE TO: dependents[t].Remove(s);
                    dependents[t].Remove(s);   // if (dependents[t].Count == 0) dependents.Remove(t);

        }

        /// <summary>
        /// Removes all existing dependencies of the form (s,r).  Then, for each
        /// t in newDependents, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents) //multiple changes.
        {
            //update dependee graph
            foreach (string key in newDependents)
                RemoveDependency(key, s);

            //reset dependents to new values of given key
            dependees[s] = new HashSet<string>(newDependents);
        }

        /// <summary>
        /// Removes all existing dependencies of the form (r,t).  Then, for each 
        /// s in newDependees, adds the dependency (s,t).
        /// Requires s != null and t != null.
        /// </summary>
        public void ReplaceDependees(string t, IEnumerable<string> newDependees) //multiple changes
        {
            //update dependent graph
            foreach (string key in newDependees)
                RemoveDependency(key, t);

            //reset dependees to new values of given key
            dependents[t] = new HashSet<string>(newDependees);
        }

    }
}
