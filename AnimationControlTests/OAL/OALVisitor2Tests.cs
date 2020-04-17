using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnimationControl.OAL;
using System;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime;
using AnimationControl.OAL;

namespace AnimationControl.OAL.Tests
{
    [TestClass()]
    public class OALVisitor2Tests
    {
        [TestMethod()]
        public void VisitExeCommandQueryCreate_normal_01()
        {
            string oalexample = "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;" +
                                    "select any dog from instances of Dog where x == (1+2)*3;" +
                                    "select any dog from instances of Dog where (1 /(obj1.x * 0.22 + object1.y * 0.55 - object1.z /2) == \"ahoj\" + \"te deti\" or (x+3 != y%4+2)) and (x<=1 or x+1>=y or obj1.x<2 or obj1.y>1.2 * cardinality observers) and (empty k or not_empty g) and not x>5;" +
                                    "create object instance of Visitor;" +
                                    "create object instance myUserAccount of UserAccount;" +
                                    "unrelate subject from observer across R15;" +
                                    "unrelate dog from owner across R7;" +
                                    "delete object instance current_user;" +
                                    "delete object instance observer3;" +
                                    "x = 15.0 * y;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "select any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "select any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) == \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "create object instance of Visitor;\n" +
                                    "create object instance myUserAccount of UserAccount;\n" +
                                    "unrelate subject from observer across R15;\n" +
                                    "unrelate dog from owner across R7;\n" +
                                    "delete object instance current_user;\n" +
                                    "delete object instance observer3;\n" +
                                    "x = 15.0 * y;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);


        }

        [TestMethod()]
        public void VisitExeCommandQueryCreate_normal_02()
        {
            string oalexample = "x = (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2);";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitExeCommandQueryCreate_normal_03()
        {
            string oalexample = "x = (obj1.x * 0.22 + object1.y * 0.55);";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "x = obj1.x * 0.22 + object1.y * 0.55;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_01()
        {
            string oalexample = "select any my_random_dog related by current_owner->Dog[R4];";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select any my_random_dog related by current_owner->Dog[R4];\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_02()
        {
            string oalexample = "select many all_my_dogs related by owner_veronica->Dog[R4];";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many all_my_dogs related by owner_veronica->Dog[R4];\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_03()
        {
            string oalexample = "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_04()
        {
            string oalexample = "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_05()
        {
            string oalexample = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1];";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1];\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_06()
        {
            string oalexample = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitEXECommandQuerySelectRelatedBy_normal_07()
        {
            string oalexample = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1]->Vols[R222]->Dra[R76]->Pson[R6]->P[R1]->Person[R176]->Person[R176]->Person[R176]->XXX[R0]->Person[R1]->MasterJMATER[R2]->Weasf[R4]->CheaT[R8]->Time[R16]->DLFG[R32]->SR[R64]->Sad[R128]->CBA[R256]->BA[R512]->A[R1024];";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1]->Vols[R222]->Dra[R76]->Pson[R6]->P[R1]->Person[R176]->Person[R176]->Person[R176]->XXX[R0]->Person[R1]->MasterJMATER[R2]->Weasf[R4]->CheaT[R8]->Time[R16]->DLFG[R32]->SR[R64]->Sad[R128]->CBA[R256]->BA[R512]->A[R1024];\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod()]
        public void VisitExeCommandQueryCreate_normal_04()
        {
            string oalexample = "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "for each dog in my_dogs\n" +
                                    "\twhile (z != 7)\n" +
                                    "\t\trelate dog to my_wife across R8;\n" +
                                    "\t\tif (not a)\n" +
                                    "\t\t\ta = true;\n" +
                                    "\t\telse\n" +
                                    "\t\t\twhile (a != false)\n" +
                                    "\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a == false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b != 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R111;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) == \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tbreak;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = zaklad + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1 + 2;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\tend for;\n" +
                                    "\t\t\t\telse\n" +
                                    "\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\tend if;\n" +
                                    "\t\t\tend while;\n" +
                                    "\t\tend if;\n" +
                                    "\t\tz = z + 1;\n" +
                                    "\tend while;\n" +
                                    "end for;\n" +
                                    "select any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "select any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) ==" +
                                    " \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "create object instance of Visitor;\n" +
                                    "create object instance myUserAccount of UserAccount;\n" +
                                    "unrelate subject from observer across R15;\n" +
                                    "unrelate dog from owner across R7;\n" +
                                    "par\n" +
                                    "\tthread\n" +
                                    "\t\twhile (x != 7)\n" +
                                    "\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                                    "\t\tend while;\n" +
                                    "\tend thread;\n" +
                                    "\tthread\n" +
                                    "\t\tcall from Clock::Update() to AnalogWidget::Update() across R10;\n" +
                                    "\tend thread;\n" +
                                    "\tthread\n" +
                                    "\t\tcall from Clock::Update() to AnalogWidget::Update() across R12;\n" +
                                    "\tend thread;\n" +
                                    "end par;\n" +
                                    "delete object instance current_user;\n" +
                                    "delete object instance observer3;\n" +
                                    "x = 15.0 * y;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "for each dog in my_dogs\n" +
                                    "\twhile (z != 7)\n" +
                                    "\t\trelate dog to my_wife across R8;\n" +
                                    "\t\tif (not a)\n" +
                                    "\t\t\ta = true;\n" +
                                    "\t\telse\n" +
                                    "\t\t\twhile (a != false)\n" +
                                    "\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a == false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b != 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R111;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) == \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tbreak;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = zaklad + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1 + 2;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\tend for;\n" +
                                    "\t\t\t\telse\n" +
                                    "\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\tend if;\n" +
                                    "\t\t\tend while;\n" +
                                    "\t\tend if;\n" +
                                    "\t\tz = z + 1;\n" +
                                    "\tend while;\n" +
                                    "end for;\n" +
                                    "select any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "select any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) ==" +
                                    " \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "create object instance of Visitor;\n" +
                                    "create object instance myUserAccount of UserAccount;\n" +
                                    "unrelate subject from observer across R15;\n" +
                                    "unrelate dog from owner across R7;\n" +
                                    "par\n" +
                                    "\tthread\n" +
                                    "\t\twhile (x != 7)\n" +
                                    "\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                                    "\t\tend while;\n" +
                                    "\tend thread;\n" +
                                    "\tthread\n" +
                                    "\t\tcall from Clock::Update() to AnalogWidget::Update() across R10;\n" +
                                    "\tend thread;\n" +
                                    "\tthread\n" +
                                    "\t\tcall from Clock::Update() to AnalogWidget::Update() across R12;\n" +
                                    "\tend thread;\n" +
                                    "end par;\n" +
                                    "delete object instance current_user;\n" +
                                    "delete object instance observer3;\n" +
                                    "x = 15.0 * y;\n";

            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);


        }

        [TestMethod()]
        public void DeathdWish()
        {
            string oalexample = "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "for each dog in my_dogs\n" +
                                    "\twhile (z != 7)\n" +
                                    "\t\trelate dog to my_wife across R8;\n" +
                                    "\t\tif (not a)\n" +
                                    "\t\t\ta = true;\n" +
                                    "\t\telse\n" +
                                    "\t\t\twhile (a != false)\n" +
                                    "\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a == false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b != 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R111;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) == \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tbreak;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = zaklad + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1 + 2;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\tend for;\n" +
                                    "\t\t\t\telse\n" +
                                    "\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\tend if;\n" +
                                    "\t\t\tend while;\n" +
                                    "\t\tend if;\n" +
                                    "\t\tz = z + 1;\n" +
                                    "\tend while;\n" +
                                    "end for;\n" +
                                    "select any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "select any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) ==" +
                                    " \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "create object instance of Visitor;\n" +
                                    "create object instance myUserAccount of UserAccount;\n" +
                                    "unrelate subject from observer across R15;\n" +
                                    "unrelate dog from owner across R7;\n" +
                                    "par\n" +
                                    "\tthread\n" +
                                    "\t\twhile (x != 7)\n" +
                                    "\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                                    "\t\tend while;\n" +
                                    "\tend thread;\n" +
                                    "\tthread\n" +
                                    "\t\tcall from Clock::Update() to AnalogWidget::Update() across R10;\n" +
                                    "\tend thread;\n" +
                                    "\tthread\n" +
                                    "\t\tcall from Clock::Update() to AnalogWidget::Update() across R12;\n" +
                                    "\tend thread;\n" +
                                    "end par;\n" +
                                    "delete object instance current_user;\n" +
                                    "delete object instance observer3;\n" +
                                    "x = 15.0 * y;\n";

            EXEScope e = Init(oalexample);

            String actualResult = e.ToCode();
            String expectedResult = "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "create object instance observer1 of Observer;\n" +
                "create object instance my_dog of Dog;\n" +
                "create object instance myUserAccount of UserAccount;\n" +
                "create object instance of Visitor;\n" +
                "relate subject to observer across R15;\n" +
                "relate dog to owner across R7;\n" +
                "select any dog from instances of Dog;\n" +
                "select many all_dogs from instances of Dog;\n" +
                "select any young_dog from instances of Dog where selected.age < 5;\n" +
                "select many young_rexos from instances of Dog where selected.age < 5 AND selected.name == \"Rex\";\n" +
                "select any my_random_dog related by current_owner->Dog[R4];\n" +
                "select many all_my_dogs related by owner_veronica->Dog[R4];\n" +
                "select any my_random_young_dog related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many my_young_dogs related by current_owner->Dog[R4] where selected.age < 5;\n" +
                "select many all_rexos_in_village related by current_village->House[R12]->Person[R176]->Dog[R1] where selected.name == \"Rex\";\n" +
                "delete object instance current_user;\n" +
                "delete object instance observer3;\n" +
                "unrelate subject from observer across R15;\n" +
                "unrelate dog from owner across R7;\n" +
                "x = 15.0 * y;\n" +
                "x = y;\n" +
                "x = \"ahoj\" + \"te\";\n" +
                "result = (x < 15 or y < 66) and is_allowed;\n" +
                "i = i + 1;\n" +
                "object.coordinate_x = 17.22;\n" +
                "archer.hp = archer.hp * 0.50 - 16;\n" +
                "dog.name = \"Koronko\";\n" +
                "door.locked = not door.key == used_key;\n" +
                "call from Observer::init() to Subject::register() across R4;\n" +
                "call from Subject::register() to Subject::addObserver();\n" +
                "call from Subject::register() to Subject::addObserver() across R7;\n" +
                "break;\n" +
                "continue;\n" +
                "x = not locked;\n" +
                "x = dog1.age > dog2.age;\n" +
                "x = 5 + 6 * 4;\n" +
                "x = x + cardinality dogs;\n" +
                "if (x == x)\n" +
                "\tx = x;\n" +
                "end if;\n" +
                "while (x != x)\n" +
                "\tx = x;\n" +
                "end while;\n" +
                "relate subject to observer across R15;\n" +
                                    "relate dog to owner across R7;\n" +
                                    "select any dog from instances of Dog;\n" +
                                    "select many all_dogs from instances of Dog;\n" +
                                    "select any young_dog from instances of Dog where selected.age < 5 OR 6;\n" +
                                    "select any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "for each dog in my_dogs\n" +
                                    "\twhile (z != 7)\n" +
                                    "\t\trelate dog to my_wife across R8;\n" +
                                    "\t\tif (not a)\n" +
                                    "\t\t\ta = true;\n" +
                                    "\t\telse\n" +
                                    "\t\t\twhile (a != false)\n" +
                                    "\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a == false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b != 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R111;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any young_dog from instances of Dog where (cardinality observers and not x > 5) or not_empty g;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tselect any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) == \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tbreak;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = zaklad + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1 + 2;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tfor each dog in my_dogs\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (z != 7)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\trelate dog to my_wife across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (not a)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = true;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tpar\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tthread\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend thread;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend par;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\twhile (a != false)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tif (b == 1)\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\ta = false;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\telse\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\t\t\t\tend for;\n" +
                                    "\t\t\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\t\t\tend if;\n" +
                                    "\t\t\t\t\t\t\tz = z + 1;\n" +
                                    "\t\t\t\t\t\tend while;\n" +
                                    "\t\t\t\t\tend for;\n" +
                                    "\t\t\t\telse\n" +
                                    "\t\t\t\t\tb = 1;\n" +
                                    "\t\t\t\tend if;\n" +
                                    "\t\t\tend while;\n" +
                                    "\t\tend if;\n" +
                                    "\t\tz = z + 1;\n" +
                                    "\tend while;\n" +
                                    "end for;\n" +
                                    "select any dog from instances of Dog where x == (1 + 2) * 3;\n" +
                                    "select any dog from instances of Dog where (1 / (obj1.x * 0.22 + object1.y * 0.55 - object1.z / 2) ==" +
                                    " \"ahoj\" + \"te deti\" or x + 3 != y % 4 + 2) and (x <= 1 or x + 1 >= y or obj1.x < 2 or obj1.y > 1.2 * cardinality observers) and (empty k or not_empty g) and not x > 5;\n" +
                                    "create object instance of Visitor;\n" +
                                    "create object instance myUserAccount of UserAccount;\n" +
                                    "unrelate subject from observer across R15;\n" +
                                    "unrelate dog from owner across R7;\n" +
                                    "par\n" +
                                    "\tthread\n" +
                                    "\t\twhile (x != 7)\n" +
                                    "\t\t\tcall from Clock::Update() to AnalogWidget::Update() across R8;\n" +
                                    "\t\tend while;\n" +
                                    "\tend thread;\n" +
                                    "\tthread\n" +
                                    "\t\tcall from Clock::Update() to AnalogWidget::Update() across R10;\n" +
                                    "\tend thread;\n" +
                                    "\tthread\n" +
                                    "\t\tcall from Clock::Update() to AnalogWidget::Update() across R12;\n" +
                                    "\tend thread;\n" +
                                    "end par;\n" +
                                    "delete object instance current_user;\n" +
                                    "delete object instance observer3;\n" +
                                    "x = 15.0 * y;\n";


            //mozme mat viac assertov
            Assert.AreEqual(expectedResult, actualResult);


        }

        private EXEScope Init(String oalexample)
        {
            ICharStream target = new AntlrInputStream(oalexample);
            ITokenSource lexer = new OALLexer(target);
            ITokenStream tokens = new CommonTokenStream(lexer);
            OALParser parser = new OALParser(tokens);
            parser.BuildParseTree = true;

            //ExprParser.LiteralContext result = parser.literal();
            OALParser.LinesContext result = parser.lines();
            Console.Write(result.ToStringTree());
            Console.WriteLine();

            OALVisitor2 test = new OALVisitor2();
            test.VisitLines(result);

            return test.globalExeScope;
        }
    }
}