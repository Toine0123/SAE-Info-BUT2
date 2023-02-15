using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



/**********************
 * cette partie n'est absolument pas fini il faut réussir a faire afficher les cartes (propriété)
 * que le joueur possède dans la zone d'affichage de ses informations
 * ********************/
namespace SAE_INFO
{
    internal class AfficheurPion : Panel
    {
        private Pion m_joueur;
        private SplitContainer top = new SplitContainer();
        private SplitContainer mid = new SplitContainer();
        private Label afficheur_nom = new Label();
        private Label argent = new Label();
        private AfficheurCarte[] possession = new AfficheurCarte[10];

        public AfficheurPion(Pion pion)
        {
            top.Orientation = Orientation.Horizontal;
            mid.Orientation = Orientation.Vertical;
            top.Visible = true;
            mid.Visible = true;
            Visible = true;
            mid.Panel2.Controls.Add(argent);
            top.Panel2.Controls.Add(mid);
            top.Panel1.Controls.Add(afficheur_nom);
            Controls.Add(top);
            m_joueur = pion;
            InitStruct();
            MAJ();
            BorderStyle = BorderStyle.FixedSingle;
        }

        private void InitStruct()
        {
            possession[0].SplitContainer1 = new SplitContainer();
            possession[0].SplitContainer2 = new SplitContainer();
            possession[0].SplitContainer3 = new SplitContainer();
            possession[0].SplitContainer4 = new SplitContainer();
            possession[0].SplitContainer1.Panel2.Controls.Add(possession[0].SplitContainer2);
            possession[0].SplitContainer2.Panel2.Controls.Add(possession[0].SplitContainer3);
            possession[0].SplitContainer3.Panel2.Controls.Add(possession[0].SplitContainer4);
            possession[0].SplitContainer1.Visible = true;
            possession[0].SplitContainer1.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(possession[0].SplitContainer1);
        }

        public void MAJ()
        {
            afficheur_nom.Text = m_joueur.GetName();
            mid.Panel1.BackColor = m_joueur.GetColor();
            argent.Text = m_joueur.GetMoney().ToString() + "€";
            
        }
    }



    class Afficheur : Panel
    {
        private AfficheurPion[] afficheurPions;

        public Afficheur(Pion[] joueurs)
        {
            Location = new Point(1000, 50);
            Size = new Size(400, 500);
            afficheurPions = new AfficheurPion[joueurs.Length];
            for(int i = 0; i< joueurs.Length; i++)
            {
                afficheurPions[i] = new AfficheurPion(joueurs[i]);
                afficheurPions[i].Size = new Size(this.Size.Width, this.Size.Height/4-30);
                afficheurPions[i].Location = new Point(0, i*10+i*afficheurPions[i].Height);
                Controls.Add(afficheurPions[i]);
            }
            MAJ();
            Visible = true;
            BorderStyle = BorderStyle.FixedSingle;
        }

        public void MAJ()
        {
            for(int i = 0; i < afficheurPions.Length; i++)
            {
                afficheurPions [i].MAJ();
            }
        }
    }

    struct AfficheurCarte
    {
        public SplitContainer SplitContainer1;
        public SplitContainer SplitContainer2;
        public SplitContainer SplitContainer3;
        public SplitContainer SplitContainer4;
    }
}
