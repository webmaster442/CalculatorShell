using System.Text;

namespace CalculatorShell.Expressions.Internals.Logic
{
    internal static class QuineMcclusky
    {
        private static Dictionary<int, List<Implicant>> Group(List<Implicant> implicants)
        {
            Dictionary<int, List<Implicant>>? group = new Dictionary<int, List<Implicant>>();
            foreach (Implicant m in implicants)
            {
                int count = Utilities.GetOneCount(m.Mask);

                if (!group.ContainsKey(count))
                    group.Add(count, new List<Implicant>());

                group[count].Add(m);
            }

            return group;
        }

        private static bool Simplify(ref List<Implicant> implicants)
        {
            //Group by number of 1's and determine relationships by comparing.
            Dictionary<int, List<Implicant>>? groups = Group(implicants).OrderBy(i => i.Key).ToDictionary(i => i.Key, i => i.Value);

            List<ImplicantRelationship>? relationships = new List<ImplicantRelationship>();
            for (int i = 0; i < groups.Keys.Count; i++)
            {
                if (i == (groups.Keys.Count - 1)) break;

                IEnumerable<ImplicantRelationship>? q = from a in groups[groups.Keys.ElementAt(i)]
                                                        from b in groups[groups.Keys.ElementAt(i + 1)]
                                                        where Utilities.GetDifferences(a.Mask, b.Mask) == 1
                                                        select new ImplicantRelationship(a, b);

                relationships.AddRange(q);
            }

            /*
             * For each relationship, find the affected minterms and remove them.
             * Then add a new implicant which simplifies the affected minterms.
             */
            foreach (ImplicantRelationship r in relationships)
            {
                List<Implicant>? rmList = new List<Implicant>();

                foreach (Implicant m in implicants)
                {
                    if (r.A.Equals(m) || r.B.Equals(m)) rmList.Add(m);
                }

                foreach (Implicant m in rmList) implicants.Remove(m);

                Implicant? newImplicant = new Implicant();
                newImplicant.Mask = Utilities.GetMask(r.A.Mask, r.B.Mask);
                newImplicant.Minterms.AddRange(r.A.Minterms);
                newImplicant.Minterms.AddRange(r.B.Minterms);

                bool exist = implicants.Any(m => m.Mask == newImplicant.Mask);

                if (!exist)
                    implicants.Add(newImplicant);
            }

            //Return true if simplification occurred, false otherwise.
            return relationships.Count != 0;
        }

        private static void PopulateMatrix(ref bool[,] matrix, List<Implicant> implicants, List<int> inputs)
        {
            for (int m = 0; m < implicants.Count; m++)
            {
                int y = implicants.IndexOf(implicants[m]);

                foreach (int i in implicants[m].Minterms)
                {
                    for (int index = 0; index < inputs.Count; index++)
                    {
                        if (i == inputs[index])
                            matrix[y, index] = true;
                    }
                }
            }
        }

        private static List<Implicant> SelectImplicants(List<Implicant> implicants, List<int> inputs)
        {
            List<int>? lstToRemove = new List<int>(inputs);
            List<Implicant>? final = new List<Implicant>();
            int runnumber = 0;
            while (lstToRemove.Count != 0)
            {
                foreach (Implicant? m in implicants)
                {
                    bool add = false;

                    if (Utilities.ContainsSubList(lstToRemove, m.Minterms))
                    {
                        add = true;
                        if (lstToRemove.Count < m.Minterms.Count) break;
                    }
                    else add = false;

                    if ((((lstToRemove.Count <= m.Minterms.Count) && !add) || runnumber > 5)
                        && Utilities.ContainsAtleastOne(lstToRemove, m.Minterms)
                        && runnumber > 5)
                    {
                        add = true;
                    }

                    if (add)
                    {
                        final.Add(m);
                        foreach (int r in m.Minterms) lstToRemove.Remove(r);
                    }
                }
                foreach (Implicant? item in final) implicants.Remove(item); //ami benne van már 1x, az még 1x ne
                ++runnumber;
            }

            return final;
        }

        private static string GetFinalExpression(List<Implicant> implicants, bool lsba = false, bool negate = false)
        {
            int longest = 0;
            StringBuilder final = new();

            if (implicants.Any())
                longest = implicants.Max(m => m.Mask.Length);

            for (int i = implicants.Count - 1; i >= 0; i--)
            {
                string s = ImplicantStringFactory.Create(implicants[i], longest, lsba, negate);
                final.Append(s);
                if (negate)
                    final.Append(" & ");
                else
                    final.Append(" | ");
            }

            string ret = final.Length > 3 ? final.ToString()[0..^3] : final.ToString();
            return ret switch
            {
                " | " => "true",
                " & " => "false",
                _ => ret,
            };
        }

        public static string GetSimplified(IEnumerable<int> care, IEnumerable<int> dontcre, int variables, QuineMcCluskeyConfig? config = null)
        {
            if (config == null)
            {
                config = new QuineMcCluskeyConfig();
            }


            List<Implicant>? implicants = new List<Implicant>();

            List<int>? all = care.Concat(dontcre).OrderBy(x => x).Distinct().ToList();


            foreach (int item in all)
            {
                Implicant? m = new Implicant
                {
                    Mask = Utilities.GetBinaryValue(item, variables),
                };
                m.Minterms.Add(item);
                implicants.Add(m);
            }

            while (Simplify(ref implicants))
            {
                //Populate a matrix.
                bool[,] matrix = new bool[implicants.Count, all.Count]; //x, y
                PopulateMatrix(ref matrix, implicants, all);
            }
            List<Implicant> selected;
            if (config.HazardFree) selected = implicants;
            else selected = SelectImplicants(implicants, care.ToList());
            return GetFinalExpression(selected, config.AIsLsb, config.Negate);
        }
    }
}
