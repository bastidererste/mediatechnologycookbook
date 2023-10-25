import openai
import pandas as pd
import re

# Initialize the OpenAI API with your key
openai.api_key = "YOUR_API_KEY"

def translate_german_to_english(german_text):
    # Using the completion endpoint to translate
    response = openai.Completion.create(
        engine="text-davinci-003",
        prompt=f"Translate the following German text to English, context is software development: '{german_text}'",
        max_tokens=150  # You can adjust this based on the length you expect
    )

    # Extracting translated text
    translated_text = response.choices[0].text.strip()
    print(translated_text)
    return translated_text

def srt_to_dataframe(srt_path):
    with open(srt_path, 'r', encoding='utf-8') as file:
        content = file.read()

    pattern = r'(?P<Index>\d+)\n(?P<Start_Time>\d{2}:\d{2}:\d{2},\d{3}) --> (?P<End_Time>\d{2}:\d{2}:\d{2},\d{3})\n(?P<Text>.*?)\n\n'
    matches = re.findall(pattern, content, re.DOTALL)

    return pd.DataFrame(matches, columns=["Index", "Start_Time", "End_Time", "Text"])

def dataframe_to_srt(df, srt_path):
    with open(srt_path, 'w', encoding='utf-8') as file:
        for _, row in df.iterrows():
            file.write(f"{row['Index']}\n")
            file.write(f"{row['Start_Time']} --> {row['End_Time']}\n")
            file.write(f"{row['Text']}\n\n")

# Load the SRT into a DataFrame
df = srt_to_dataframe('Text_TechTalk_Part1.srt')

# Translate the "Text" column
df['Text'] = df['Text'].apply(translate_german_to_english)

# Save the DataFrame with translations back to a new SRT
dataframe_to_srt(df, 'translated_file.srt')
