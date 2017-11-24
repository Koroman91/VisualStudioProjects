#pragma once

namespace ImageProcessing {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;
	using namespace System::Drawing::Imaging;

	/// <summary>
	/// Summary for Form1
	/// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Bitmap^ bmpFront;
		unsigned char* bmpOriginal;
		static int imageSizeInBytes = -1;
		static Rectangle imageRectangle;
		BitmapData ^bmpData;
		static double cppCount = 0.0;
		static double cppTotal = 0.0;
		
		void AdjustBrightness(unsigned char* bmp, short amount)
		{
			for(int i = 0; i < imageSizeInBytes; i++)
			{
				if((short) bmpOriginal[i] + amount < 0) bmp[i] = 0;
				else if((short) bmpOriginal[i] + amount > 255) bmp[i] = 255;
				else bmp[i] = bmpOriginal[i] + amount;
			}
		}

		void ClearOriginalImage()
		{
			if(bmpOriginal != nullptr)
				delete[] bmpOriginal;
		}

		// Pravi kopiju originalne slike

		void SaveOriginalImage(System::Drawing::Bitmap ^bmp)
		{
			ClearOriginalImage();

			imageSizeInBytes = bmp->Width * bmp->Height * 3;

			bmpOriginal = new unsigned char [imageSizeInBytes];

			imageRectangle.Width = bmp->Width;
			imageRectangle.Height = bmp->Height;

			bmpData = bmp->LockBits(imageRectangle, ImageLockMode::ReadOnly, PixelFormat::Format24bppRgb);

			unsigned char*p = (unsigned char*) bmpData->Scan0.ToPointer();

			for (int i=0; i<imageSizeInBytes; i++)
			{
				bmpOriginal[i] = *p++;
			}
			bmp->UnlockBits(bmpData);
		}

		Form1(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~Form1()
		{
			if (components)
			{
				delete components;
			}
		}
	private: System::Windows::Forms::MenuStrip^  menuStrip1;
	protected: 
	private: System::Windows::Forms::ToolStripMenuItem^  fileToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^  openToolStripMenuItem;
	private: System::Windows::Forms::ToolStripMenuItem^  exitToolStripMenuItem;
	private: System::Windows::Forms::PictureBox^  picImage;
	private: System::Windows::Forms::TrackBar^  trkBrightness;
	private: System::Windows::Forms::Label^  lblCPPAverage;
	private: System::Windows::Forms::OpenFileDialog^  dlgOpen;


	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->menuStrip1 = (gcnew System::Windows::Forms::MenuStrip());
			this->fileToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->openToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->exitToolStripMenuItem = (gcnew System::Windows::Forms::ToolStripMenuItem());
			this->picImage = (gcnew System::Windows::Forms::PictureBox());
			this->trkBrightness = (gcnew System::Windows::Forms::TrackBar());
			this->lblCPPAverage = (gcnew System::Windows::Forms::Label());
			this->dlgOpen = (gcnew System::Windows::Forms::OpenFileDialog());
			this->menuStrip1->SuspendLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->picImage))->BeginInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->trkBrightness))->BeginInit();
			this->SuspendLayout();
			// 
			// menuStrip1
			// 
			this->menuStrip1->Items->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(1) {this->fileToolStripMenuItem});
			this->menuStrip1->Location = System::Drawing::Point(0, 0);
			this->menuStrip1->Name = L"menuStrip1";
			this->menuStrip1->Size = System::Drawing::Size(497, 24);
			this->menuStrip1->TabIndex = 0;
			this->menuStrip1->Text = L"menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this->fileToolStripMenuItem->DropDownItems->AddRange(gcnew cli::array< System::Windows::Forms::ToolStripItem^  >(2) {this->openToolStripMenuItem, 
				this->exitToolStripMenuItem});
			this->fileToolStripMenuItem->Name = L"fileToolStripMenuItem";
			this->fileToolStripMenuItem->Size = System::Drawing::Size(37, 20);
			this->fileToolStripMenuItem->Text = L"File";
			// 
			// openToolStripMenuItem
			// 
			this->openToolStripMenuItem->Name = L"openToolStripMenuItem";
			this->openToolStripMenuItem->Size = System::Drawing::Size(103, 22);
			this->openToolStripMenuItem->Text = L"Open";
			this->openToolStripMenuItem->Click += gcnew System::EventHandler(this, &Form1::openToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this->exitToolStripMenuItem->Name = L"exitToolStripMenuItem";
			this->exitToolStripMenuItem->Size = System::Drawing::Size(103, 22);
			this->exitToolStripMenuItem->Text = L"Exit";
			this->exitToolStripMenuItem->Click += gcnew System::EventHandler(this, &Form1::exitToolStripMenuItem_Click);
			// 
			// picImage
			// 
			this->picImage->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((((System::Windows::Forms::AnchorStyles::Top | System::Windows::Forms::AnchorStyles::Bottom) 
				| System::Windows::Forms::AnchorStyles::Left) 
				| System::Windows::Forms::AnchorStyles::Right));
			this->picImage->Location = System::Drawing::Point(12, 27);
			this->picImage->Name = L"picImage";
			this->picImage->Size = System::Drawing::Size(266, 228);
			this->picImage->SizeMode = System::Windows::Forms::PictureBoxSizeMode::Zoom;
			this->picImage->TabIndex = 1;
			this->picImage->TabStop = false;
			// 
			// trkBrightness
			// 
			this->trkBrightness->Anchor = static_cast<System::Windows::Forms::AnchorStyles>(((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Left) 
				| System::Windows::Forms::AnchorStyles::Right));
			this->trkBrightness->Enabled = false;
			this->trkBrightness->Location = System::Drawing::Point(14, 273);
			this->trkBrightness->Maximum = 255;
			this->trkBrightness->Minimum = -255;
			this->trkBrightness->Name = L"trkBrightness";
			this->trkBrightness->Size = System::Drawing::Size(138, 45);
			this->trkBrightness->TabIndex = 2;
			this->trkBrightness->TickFrequency = 16;
			this->trkBrightness->Scroll += gcnew System::EventHandler(this, &Form1::trkBrightness_Scroll);
			// 
			// lblCPPAverage
			// 
			this->lblCPPAverage->Anchor = static_cast<System::Windows::Forms::AnchorStyles>((System::Windows::Forms::AnchorStyles::Bottom | System::Windows::Forms::AnchorStyles::Right));
			this->lblCPPAverage->AutoSize = true;
			this->lblCPPAverage->Location = System::Drawing::Point(325, 273);
			this->lblCPPAverage->Name = L"lblCPPAverage";
			this->lblCPPAverage->Size = System::Drawing::Size(97, 20);
			this->lblCPPAverage->TabIndex = 3;
			this->lblCPPAverage->Text = L"C++Average";
			// 
			// dlgOpen
			// 
			this->dlgOpen->FileName = L"openFileDialog1";
			this->dlgOpen->Filter = L"Jpeg|*.jpg|Bitmap|*.bmp|All Files|\".\"";
			this->dlgOpen->FilterIndex = 2;
			// 
			// Form1
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(9, 20);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(497, 403);
			this->Controls->Add(this->lblCPPAverage);
			this->Controls->Add(this->trkBrightness);
			this->Controls->Add(this->picImage);
			this->Controls->Add(this->menuStrip1);
			this->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 12, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				static_cast<System::Byte>(0)));
			this->MainMenuStrip = this->menuStrip1;
			this->Margin = System::Windows::Forms::Padding(4, 5, 4, 5);
			this->Name = L"Form1";
			this->Text = L"ImageProcessing";
			this->FormClosing += gcnew System::Windows::Forms::FormClosingEventHandler(this, &Form1::Form1_FormClosing);
			this->menuStrip1->ResumeLayout(false);
			this->menuStrip1->PerformLayout();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->picImage))->EndInit();
			(cli::safe_cast<System::ComponentModel::ISupportInitialize^  >(this->trkBrightness))->EndInit();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void exitToolStripMenuItem_Click(System::Object^  sender, System::EventArgs^  e) 
			 {
				 Application::Exit();
			 }
private: System::Void openToolStripMenuItem_Click(System::Object^  sender, System::EventArgs^  e)
		 {
			 if(dlgOpen->ShowDialog() == System::Windows::Forms::DialogResult::OK)
			 {
				 try
				 {
					 bmpFront = (Bitmap^) Image::FromFile(dlgOpen->FileName);
					 SaveOriginalImage(bmpFront);
					 picImage->Image = bmpFront;

					 trkBrightness->Enabled = true;

					 cppTotal = 0.0;
					 cppCount = 0.0;
				 }
				 catch(...)
				 {
					 MessageBox::Show("Ovaj fajl ne moze biti otvoren!");
				 }
			 }
		 }
private: System::Void trkBrightness_Scroll(System::Object^  sender, System::EventArgs^  e) 
		 {
			 bmpData = bmpFront->LockBits(imageRectangle, ImageLockMode::WriteOnly, PixelFormat::Format24bppRgb);
			 
			 long startTime = clock();

			 AdjustBrightness((unsigned char*)  bmpData->Scan0.ToPointer(),
				 trkBrightness->Value);

			 long finishTime = clock();

			 bmpFront->UnlockBits(bmpData);

			 picImage->Image = bmpFront;

			 cppTotal += finishTime - startTime;
			 cppCount++;

			 lblCPPAverage->Text = "C++ Average: " + Math::Round(cppTotal / cppCount, 2);

		 }
private: System::Void Form1_FormClosing(System::Object^  sender, System::Windows::Forms::FormClosingEventArgs^  e) 
		 {
			 ClearOriginalImage();
		 }
};
}

// Filters:
// Desc|Search Option|Description| Search
// Jpeg|*.jpg|Bitmap|*.bmp|All Files|"."